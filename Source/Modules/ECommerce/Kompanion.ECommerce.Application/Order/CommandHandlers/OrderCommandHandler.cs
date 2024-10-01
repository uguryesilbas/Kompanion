using Kompanion.Application.Exceptions;
using Kompanion.Application.Extensions;
using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.Wrappers;
using Kompanion.ECommerce.Application.Order.Commands;
using Kompanion.ECommerce.Application.Order.Models;
using Kompanion.ECommerce.Domain.Order;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;

namespace Kompanion.ECommerce.Application.Order.CommandHandlers;

public class OrderCommandHandler : ICommandHandler<CreateOrderCommand, ApiResponse>
{
    private readonly IOrderRepository _orderRepository;

    public OrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ApiResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //MySQL de Dictionary mantığında çalışan bir şey bulamadığımdan dolayı loop ile yapıldı. 
            foreach (CreateOrderDetailModel order in request.Orders)
            {
                //Clean architecture mimarisinden dolayı entity'e çevirilerek gönderildi.
                OrderDetailEntity orderDetail = OrderDetailEntity.CreateNew(0, order.ProductId, 0, order.Quantity);

                await _orderRepository.CreateOrder(request.CountryId, orderDetail, cancellationToken);
            }

            return new ApiResponse().Ok();
        }
        catch (MySqlException exception)
        {
            throw new HandledException(exception, StatusCodes.Status400BadRequest, exception.Message);
        }
    }
}

