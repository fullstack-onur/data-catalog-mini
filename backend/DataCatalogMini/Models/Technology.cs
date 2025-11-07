namespace DataCatalogMini.Models
{
    public class Technology
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation
        public ICollection<Asset>? Assets { get; set; }
    }
}
