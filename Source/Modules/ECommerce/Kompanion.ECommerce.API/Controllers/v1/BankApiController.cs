using Asp.Versioning;
using Kompanion.Application;
using Kompanion.Application.Controllers;
using Kompanion.Application.Wrappers;
using Kompanion.ECommerce.Application.Bank.Commands;
using Kompanion.ECommerce.Application.Bank.Dtos;
using Kompanion.ECommerce.Application.Bank.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Kompanion.ECommerce.API.Controllers.v1;


[ApiVersion(ApplicationConstants.ApiVersioningConstants.DefaultApiVersion)]
[Route($"{DefaultApiRoute}/{ControllerNameRoute}")]
[ApiController]
//[Authorize]
public class BankApiController(ISender sender) : BaseApiController(sender)
{
    private const string ControllerNameRoute = "banks";

    /// <summary>
    /// Seçilen bankayı getirir.
    /// </summary>
    /// <param name="bankId">Banka id</param>
    /// <returns></returns>
    /// <response code="400">Model'de hatalı ya da işlem gerçekleştirilirken hata oluştu.</response> 
    /// <response code="404">Banka bulunamadı</response>
    /// <response code="403">Client'ın erişemediği kanal, claim ya da roleEntity.</response>
    [HttpGet("{bankId}")]
    [ProducesResponseType(typeof(ApiResponse<BankDto>), Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    [ProducesResponseType(Status403Forbidden)]
    public async Task<IActionResult> Get([FromRoute] int bankId)
    {
        return await Send(new GetBankQuery(bankId));
    }

    /// <summary>
    /// Banka oluşturur.
    /// </summary> 
    /// <param name="command">Banka bilgileri</param>
    /// <returns></returns>  
    /// <response code="201">Banka oluşturuldu.</response>
    /// <response code="400">Model'de hatalı ya da işlem gerçekleştirilirken hata oluştu.</response>
    /// <response code="403">Client'ın erişemediği kanal, claim ya da roleEntity.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<int>), Status201Created)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status403Forbidden)]
    public async Task<IActionResult> Create([FromBody] CreateBankCommand command)
    {
        return await Send(command);
    }
}

