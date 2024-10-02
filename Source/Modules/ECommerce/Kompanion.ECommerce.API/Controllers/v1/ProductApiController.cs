using Asp.Versioning;
using Kompanion.Application;
using Kompanion.Application.Controllers;
using Kompanion.Application.Wrappers;
using Kompanion.ECommerce.Application.Product.Commands;
using Kompanion.ECommerce.Application.Product.Dtos;
using Kompanion.ECommerce.Application.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Kompanion.ECommerce.API.Controllers.v1;


[ApiVersion(ApplicationConstants.ApiVersioningConstants.DefaultApiVersion)]
[Route($"{DefaultApiRoute}/{ControllerNameRoute}")]
[ApiController]
[Authorize]
public class ProductApiController(ISender sender) : BaseApiController(sender)
{
    private const string ControllerNameRoute = "products";


    /// <summary>
    /// Ürünü getirir.
    /// </summary>
    /// <param name="productId">Ürün id</param>
    /// <returns></returns>
    /// <response code="400">Model'de hatalı ya da işlem gerçekleştirilirken hata oluştu.</response> 
    /// <response code="404">Ürün bulunamadı</response>
    /// <response code="403">Client'ın erişemediği kanal, claim ya da roleEntity.</response>
    [HttpGet("{productId}")]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    [ProducesResponseType(Status403Forbidden)]
    public async Task<IActionResult> Get([FromRoute] int productId)
    {
        return await Send(new GetProductQuery(productId));
    }

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
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        return await Send(command);
    }

    /// <summary>
    /// Ürünü günceller.
    /// </summary> 
    /// <param name="productId">Ürün id</param>
    /// <param name="command">Ürün bilgileri</param>
    /// <returns></returns>  
    /// <response code="400">Model'de hatalı ya da işlem gerçekleştirilirken hata oluştu.</response>
    /// <response code="403">Client'ın erişemediği kanal, claim ya da roleEntity.</response>
    /// <response code="404">Ürün bulunamadı</response> 
    [HttpPut("{productId}")]
    [ProducesResponseType(typeof(ApiResponse), Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    [ProducesResponseType(Status403Forbidden)]
    public async Task<IActionResult> Update([FromRoute] int productId, [FromBody] UpdateProductCommand command)
    {
        return await Send(command with { Id = productId });
    }

    /// <summary>
    /// Ürünü siler.
    /// </summary>
    /// <param name="productId">Ürün id</param>
    /// <returns></returns>
    /// <response code="400">Model'de hatalı ya da işlem gerçekleştirilirken hata oluştu.</response>
    /// <response code="403">Client'ın erişemediği kanal, claim ya da roleEntity.</response>
    /// <response code="404">Ürün bulunamadı</response>
    [HttpDelete("{productId}")]
    [ProducesResponseType(typeof(ApiResponse), Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status403Forbidden)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<IActionResult> DeleteById([FromRoute] int productId)
    {
        return await Send(new DeleteProductCommand(productId));
    }


    /// <summary>
    /// Ürün fiyatı oluşturur.
    /// </summary> 
    /// <param name="command">Ürün fiyat bilgileri</param>
    /// <returns></returns>  
    /// <response code="201">Ürün fiyatı oluşturuldu.</response>
    /// <response code="400">Model'de hatalı ya da işlem gerçekleştirilirken hata oluştu.</response>
    /// <response code="403">Client'ın erişemediği kanal, claim ya da roleEntity.</response>
    /// <response code="404">Ürün bulunamadı</response>
    [HttpPost("prices")]
    [ProducesResponseType(typeof(ApiResponse<int>), Status201Created)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    [ProducesResponseType(Status403Forbidden)]
    public async Task<IActionResult> CreatePrice([FromBody] CreateProductPriceCommand command)
    {
        return await Send(command);
    }

    /// <summary>
    /// Ürün fiyatı günceller.
    /// </summary> 
    /// <param name="productPriceId">Ürün id</param>
    /// <param name="command">Ürün fiyat bilgileri</param>
    /// <returns></returns>  
    /// <response code="400">Model'de hatalı ya da işlem gerçekleştirilirken hata oluştu.</response>
    /// <response code="403">Client'ın erişemediği kanal, claim ya da roleEntity.</response>
    /// <response code="404">Ürün fiyatı bulunamadı</response> 
    [HttpPut("prices/{productPriceId}")]
    [ProducesResponseType(typeof(ApiResponse), Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    [ProducesResponseType(Status403Forbidden)]
    public async Task<IActionResult> UpdatePrice([FromRoute] int productPriceId, [FromBody] UpdateProductPriceCommand command)
    {
        return await Send(command with { Id = productPriceId });
    }


    /// <summary>
    /// Ürün fiyatı siler.
    /// </summary>
    /// <param name="productPriceId">Ürün fiyat id</param>
    /// <returns></returns>
    /// <response code="400">Model'de hatalı ya da işlem gerçekleştirilirken hata oluştu.</response>
    /// <response code="403">Client'ın erişemediği kanal, claim ya da roleEntity.</response>
    /// <response code="404">Ürün fiyatı bulunamadı</response>
    [HttpDelete("prices/{productPriceId}")]
    [ProducesResponseType(typeof(ApiResponse), Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status403Forbidden)]
    [ProducesResponseType(Status404NotFound)]
    public async Task<IActionResult> DeletePriceById([FromRoute] int productPriceId)
    {
        return await Send(new DeleteProductPriceCommand(productPriceId));
    }
}

