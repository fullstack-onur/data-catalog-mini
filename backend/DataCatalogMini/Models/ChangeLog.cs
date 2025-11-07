namespace DataCatalogMini.Models
{
    public class ChangeLog
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime ChangeDate { get; set; } = DateTime.UtcNow;

        // Navigation
        public Asset? Asset { get; set; }
        public User? User { get; set; }
    }
}
