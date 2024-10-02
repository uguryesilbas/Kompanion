using Kompanion.Application.Enums;
using Kompanion.Application.Exceptions;
using Kompanion.Application.Wrappers;
using Kompanion.Domain.Extensions;
using Kompanion.ECommerce.Application.Invoice.Commands;
using Kompanion.ECommerce.Application.Notification.Commands;
using Kompanion.ECommerce.Application.Payment.Commands;
using Kompanion.ECommerce.Application.Payment.Models;
using Kompanion.ECommerce.Domain.Order;
using Kompanion.ECommerce.Domain.Order.Enums;
using Kompanion.ECommerce.Domain.PaymentRule;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Kompanion.ECommerce.Infrastructure.Saga.Order;

public sealed class OrderProcessSagaService : IOrderProcessSagaService
{
    private const PaymentBankType DefaultBank = PaymentBankType.ABank;

    private readonly ISender _sender;
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IPaymentRuleRepository _paymentRuleRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderProcessSagaService(IOrderDetailRepository orderDetailRepository, IPaymentRuleRepository paymentRuleRepository, ISender sender, IOrderRepository orderRepository = null)
    {
        _orderDetailRepository = orderDetailRepository;
        _paymentRuleRepository = paymentRuleRepository;
        _sender = sender;
        _orderRepository = orderRepository;
    }

    public async Task ProcessOrderAsync(OrderEntity order, CancellationToken cancellationToken = default)
    {
        List<PaymentOrderDetailModel> paymentOrderDetails = await GetOrdersToBePaid(order.Id, order.CountryId, cancellationToken);

        ApiResponse paymentResult = await _sender.Send(new ProcessPaymentCommand { OrderDetails = paymentOrderDetails }, cancellationToken);

        if (!paymentResult.IsSuccessStatusCode)
        {
            await _orderRepository.UpdateStatusAsync(order.Id, OrderStatusType.Fail, cancellationToken);

            throw new HandledException(paymentResult.HttpStatusCode, "Ödeme alınamadı!");
        }

        ApiResponse invoiceResult = await _sender.Send(new CreateInvoiceCommand(), cancellationToken);

        if (!invoiceResult.IsSuccessStatusCode)
        {
            await _sender.Send(new RefundPaymentCommand { OrderDetails = paymentOrderDetails }, cancellationToken); // Ödeme iadesi

            await _orderRepository.UpdateStatusAsync(order.Id, OrderStatusType.Fail, cancellationToken);

            throw new HandledException(invoiceResult.HttpStatusCode, "Fatura oluşturulamadı!");
        }

        ApiResponse emailResult = await _sender.Send(new SendEmailCommand { Email = "kompanion" }, cancellationToken);

        if (!emailResult.IsSuccessStatusCode)
        {
            await _sender.Send(new CancelInvoiceCommand()); // Fatura iptali

            await _sender.Send(new RefundPaymentCommand { OrderDetails = paymentOrderDetails }, cancellationToken); // Ödeme iadesi

            await _orderRepository.UpdateStatusAsync(order.Id, OrderStatusType.Fail, cancellationToken);

            throw new HandledException(emailResult.HttpStatusCode, "Email gönderilemedi!");
        }

        await _orderRepository.UpdateStatusAsync(order.Id, OrderStatusType.Completed, cancellationToken);
    }

    private async Task<List<PaymentOrderDetailModel>> GetOrdersToBePaid(int orderId, int countryId, CancellationToken cancellationToken)
    {
        List<PaymentOrderDetailModel> paymentOrderDetails = new();

        List<OrderDetailEntity> orderDetails = await _orderDetailRepository.GetOrderDetailsByOrderId(orderId, cancellationToken);

        List<int> productIds = orderDetails.Select(x => x.ProductId).Distinct().ToList();

        List<PaymentRuleEntity> paymentRules = await _paymentRuleRepository.GetPaymentRulesByProductIdsAsync(productIds, countryId, cancellationToken);

        // PaymentRule olmayan sipariş detaylarını filtrele
        List<OrderDetailEntity> orderDetailsWithoutPaymentRule = orderDetails
            .Where(x => paymentRules is null || !paymentRules.Select(p => p.ProductId).Contains(x.ProductId))
            .ToList();

        if (orderDetailsWithoutPaymentRule is { Count: > 0 })
        {
            paymentOrderDetails.Add(new PaymentOrderDetailModel { Bank = DefaultBank, TotalAmount = orderDetailsWithoutPaymentRule.Sum(x => x.PriceAtPurchase) });

            orderDetails = orderDetails.Except(orderDetailsWithoutPaymentRule).ToList();
        }

        if (orderDetails is not { Count: > 0 }) // Eğer kalan sipariş detayı yoksa dön
        {
            return paymentOrderDetails;
        }

        // PaymentRule'ları kullanarak kalan sipariş detaylarını işle
        foreach (OrderDetailEntity orderDetail in orderDetails)
        {
            var paymentRule = paymentRules.FirstOrDefault(x => x.ProductId == orderDetail.ProductId);

            if (paymentRule == null)
            {
                paymentOrderDetails.Add(new PaymentOrderDetailModel { Bank = DefaultBank, TotalAmount = orderDetail.PriceAtPurchase });
            };

            bool isWithinPriceRange = paymentRule.MinAmount.HasValue && paymentRule.MaxAmount.HasValue &&
                orderDetail.PriceAtPurchase >= paymentRule.MinAmount.Value &&
                orderDetail.PriceAtPurchase <= paymentRule.MaxAmount.Value;

            bool isWithinDateRange = paymentRule.StartDateTime.HasValue && paymentRule.EndDateTime.HasValue &&
                DateTimeExtensions.Now >= paymentRule.StartDateTime.Value &&
                DateTimeExtensions.Now <= paymentRule.EndDateTime.Value;

            if (isWithinPriceRange || isWithinDateRange)
            {
                paymentOrderDetails.Add(new PaymentOrderDetailModel { Bank = (PaymentBankType)paymentRule.BankId, TotalAmount = orderDetail.PriceAtPurchase });
            }
        }

        return paymentOrderDetails;
    }
}
