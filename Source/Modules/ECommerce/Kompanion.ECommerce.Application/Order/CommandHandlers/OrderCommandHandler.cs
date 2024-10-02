using Kompanion.Application.Exceptions;
using Kompanion.Application.Extensions;
using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.Wrappers;
using Kompanion.ECommerce.Application.Order.Commands;
using Kompanion.ECommerce.Application.Payment.Commands;
using Kompanion.ECommerce.Domain.Order;
using Kompanion.ECommerce.Infrastructure.Saga.Order;
using MediatR;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;

namespace Kompanion.ECommerce.Application.Order.CommandHandlers;

public class OrderCommandHandler : ICommandHandler<CreateOrderCommand, ApiResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderProcessSagaService _orderProcessService;

    public OrderCommandHandler(IOrderRepository orderRepository, IOrderProcessSagaService orderProcessService)
    {
        _orderRepository = orderRepository;
        _orderProcessService = orderProcessService;
    }

    public async Task<ApiResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //Clean architecture mimarisinden dolayı entity'e çevirilerek gönderildi.

            List<OrderDetailEntity> orderDetails = request.Orders.Select(x => OrderDetailEntity.CreateNew(0, x.ProductId, 0, x.Quantity, string.Empty)).ToList();

            OrderEntity order = await _orderRepository.CreateOrder(request.CountryId, orderDetails, cancellationToken);

            await _orderProcessService.ProcessOrderAsync(order, cancellationToken);

            return new ApiResponse().Ok();
        }
        catch (MySqlException exception)
        {
            throw new HandledException(exception, StatusCodes.Status400BadRequest, exception.Message);
        }
    }
}

