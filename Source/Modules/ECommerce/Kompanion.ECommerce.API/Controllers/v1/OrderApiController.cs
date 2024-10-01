using Asp.Versioning;
using Kompanion.Application;
using Kompanion.Application.Controllers;
using Kompanion.Application.Wrappers;
using Kompanion.ECommerce.Application.Order.Commands;
using Kompanion.ECommerce.Application.Product.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Kompanion.ECommerce.API.Controllers.v1;


[ApiVersion(ApplicationConstants.ApiVersioningConstants.DefaultApiVersion)]
[Route($"{DefaultApiRoute}/{ControllerNameRoute}")]
[ApiController]
//[Authorize]
public class OrderApiController(ISender sender) : BaseApiController(sender)
{
    private const string ControllerNameRoute = "Orders";


    /// <summary>
    /// Ürün oluşturur.
    /// </summary> 
    /// <param name="command">Ürün bilgileri</param>
    /// <returns></returns>  
    /// <response code="201">Ürün oluşturuldu.</response>
    /// <response code="400">Model'de hatalı ya da işlem gerçekleştirilirken hata oluştu.</response>
    /// <response code="403">Client'ın erişemediği kanal, claim ya da roleEntity.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<int>), Status201Created)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status403Forbidden)]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
    {
        return await Send(command);
    }
}

