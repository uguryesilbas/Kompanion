using Microsoft.AspNetCore.Mvc;

namespace Kompanion.ECommerce.API.Models
{
    public record UserTokenInfo 
    {
        public string AccessToken { get; init; }
        public DateTime AccessTokenExpiration { get; init; }
    }
}