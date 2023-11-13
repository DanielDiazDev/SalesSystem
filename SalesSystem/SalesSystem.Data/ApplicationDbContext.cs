using Microsoft.EntityFrameworkCore;
using SalesSystem.Model;

namespace SalesSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)"); // Ejemplo: decimal con 18 dígitos en total y 2 decimales

            modelBuilder.Entity<SaleDetail>()
                .Property(sd => sd.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<SaleDetail>()
                .Property(sd => sd.Total)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Sale>()
                .Property(s => s.Total)
                .HasColumnType("decimal(18, 2)");
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DocumentNumber> DocumentNumbers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<User> Users { get; set; }
    }
}