namespace Kompanion.ECommerce.API.Models
{
    public record UserLogin
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
