namespace DataCatalogMini.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        // FK
        public int TechnologyId { get; set; }
        public Technology? Technology { get; set; }

        // Navigation
        public ICollection<ChangeLog>? ChangeLogs { get; set; }
    }
}
