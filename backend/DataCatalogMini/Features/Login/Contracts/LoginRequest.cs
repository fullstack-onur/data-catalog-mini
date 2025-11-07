namespace DataCatalogMini.Features.Login.Contracts
{
    public class LoginRequest
    {
        public string Id { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
