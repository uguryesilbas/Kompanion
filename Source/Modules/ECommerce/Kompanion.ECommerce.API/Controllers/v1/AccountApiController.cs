using Asp.Versioning;
using Kompanion.Application;
using Kompanion.Application.Controllers;
using Kompanion.Domain.Extensions;
using Kompanion.ECommerce.API.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Kompanion.ECommerce.API.Controllers.v1;

[ApiVersion(ApplicationConstants.ApiVersioningConstants.DefaultApiVersion)]
[Route($"{DefaultApiRoute}/{ControllerNameRoute}")]
[ApiController]
public class AccountApiController(ISender sender) : BaseApiController(sender)
{
    private const string ControllerNameRoute = "accounts";
    private const string SecurityKey = "KAhOpbxKPNLK03DuQqq1pfXB3tKZQ8rc";


    /// <summary>
    ///  Login (Ayrı bir identity modülü veya servisi yazmadım. İhtiyaç duyulursa onu da yazabilirim :) Şu anlık static)
    /// </summary>
    /// <param name="request">admin admin</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(UserTokenInfo), Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] UserLogin request)
    {
        if (request.Username != "admin" || request.Password != "admin")
        {
            return BadRequest();
        }

        DateTime accessTokenExpiration = DateTimeExtensions.Now.AddMinutes(10);

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(SecurityKey));

        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);

        IReadOnlyCollection<Claim> claims = await GetClaims(request.Username);

        JwtSecurityToken jwtSecurityToken = new(
            issuer: "ECommerce",
            audience: "ECommerce",
            expires: accessTokenExpiration,
            notBefore: DateTimeExtensions.Now,
            signingCredentials: signingCredentials,
            claims: claims
        );

        string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return Ok(new UserTokenInfo
        {
            AccessToken = token,
            AccessTokenExpiration = accessTokenExpiration
        });
    }

    private Task<ReadOnlyCollection<Claim>> GetClaims(string userName)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Name, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        return Task.FromResult(claims.AsReadOnly());
    }
}

