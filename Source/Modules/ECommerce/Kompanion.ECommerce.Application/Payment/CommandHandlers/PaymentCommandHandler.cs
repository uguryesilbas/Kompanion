using Kompanion.Application.Abstractions;
using Kompanion.Application.Enums;
using Kompanion.Application.Extensions;
using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.Wrappers;
using Kompanion.ECommerce.Application.Payment.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Kompanion.ECommerce.Application.Payment.CommandHandlers;

public class PaymentCommandHandler : ICommandHandler<ProcessPaymentCommand, ApiResponse>, ICommandHandler<RefundPaymentCommand, ApiResponse>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<PaymentCommandHandler> _logger;

    public PaymentCommandHandler(IServiceProvider serviceProvider, ILogger<PaymentCommandHandler> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task<ApiResponse> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Dictionary<PaymentBankType, decimal> ordersToBePaid = request.OrderDetails.GroupBy(x => x.Bank).ToDictionary(x => x.Key, p => p.Sum(s => s.TotalAmount));

            List<Task> paymentTaskList = new();

            foreach (KeyValuePair<PaymentBankType, decimal> order in ordersToBePaid)
            {
                IPaymentService paymentService = _serviceProvider.GetRequiredKeyedService<IPaymentService>(order.Key);

                paymentTaskList.Add(paymentService.ReceivePaymentAsync(123, "09/27", "321", order.Value, cancellationToken));
            }

            await Task.WhenAll(paymentTaskList);

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Ödeme alınamadı");

            return new ApiResponse().BadRequest();
        }

        return new ApiResponse().Ok();
    }

    public async Task<ApiResponse> Handle(RefundPaymentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Dictionary<PaymentBankType, decimal> ordersToBePaid = request.OrderDetails.GroupBy(x => x.Bank).ToDictionary(x => x.Key, p => p.Sum(s => s.TotalAmount));

            List<Task> paymentTaskList = new();

            foreach (KeyValuePair<PaymentBankType, decimal> order in ordersToBePaid)
            {
                IPaymentService paymentService = _serviceProvider.GetRequiredKeyedService<IPaymentService>(order.Key);

                paymentTaskList.Add(paymentService.RefundPaymentAsync(123, "09/27", "321", order.Value, cancellationToken));
            }

            await Task.WhenAll(paymentTaskList);

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Ödeme iade edilemedi!");

            return new ApiResponse().BadRequest();
        }

        return new ApiResponse().Ok();
    }
}

