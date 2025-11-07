namespace DataCatalogMini.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "User";

        // Navigation
        public ICollection<ChangeLog>? ChangeLogs { get; set; }
    }
}
