using DataCatalogMini.Models;
using Microsoft.EntityFrameworkCore;

namespace DataCatalogMini.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Asset> Assets => Set<Asset>();
        public DbSet<Technology> Technologies => Set<Technology>();
        public DbSet<ChangeLog> ChangeLogs => Set<ChangeLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relations
            modelBuilder.Entity<Asset>()
                .HasOne(a => a.Technology)
                .WithMany(t => t.Assets)
                .HasForeignKey(a => a.TechnologyId);

            modelBuilder.Entity<ChangeLog>()
                .HasOne(c => c.Asset)
                .WithMany(a => a.ChangeLogs)
                .HasForeignKey(c => c.AssetId);

            modelBuilder.Entity<ChangeLog>()
                .HasOne(c => c.User)
                .WithMany(u => u.ChangeLogs)
                .HasForeignKey(c => c.UserId);

            // Seed Data
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Password = "admin", Role = "Admin" },
                new User { Id = 2, Username = "onur", Password = "1234", Role = "User" }
            );

            modelBuilder.Entity<Technology>().HasData(
                new Technology { Id = 1, Name = "Oracle" },
                new Technology { Id = 2, Name = "PostgreSQL" },
                new Technology { Id = 3, Name = "Kafka" },
                new Technology { Id = 4, Name = "SAP HANA" }
            );

            modelBuilder.Entity<Asset>().HasData(
                new Asset { Id = 1, Name = "Customer_Records", Type = "Table", Owner = "Data Governance", LastUpdated = DateTime.UtcNow.AddDays(-5), TechnologyId = 1 },
                new Asset { Id = 2, Name = "Transactions_Stream", Type = "Topic", Owner = "Core Banking", LastUpdated = DateTime.UtcNow.AddDays(-2), TechnologyId = 3 },
                new Asset { Id = 3, Name = "HR_Employees", Type = "Table", Owner = "HR Department", LastUpdated = DateTime.UtcNow.AddDays(-1), TechnologyId = 2 }
            );

            modelBuilder.Entity<ChangeLog>().HasData(
                new ChangeLog { Id = 1, AssetId = 1, UserId = 1, Description = "Column added: customer_status", ChangeDate = DateTime.UtcNow.AddDays(-3) },
                new ChangeLog { Id = 2, AssetId = 2, UserId = 2, Description = "New partition policy applied", ChangeDate = DateTime.UtcNow.AddDays(-1) }
            );
        }
    }
}
