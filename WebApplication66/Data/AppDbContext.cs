using ClientManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientManagement.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>().HasData(
                new Client { Id = 1, FullName = "Іван Петренко", Email = "ivan@example.com", Phone = "+380501112233", RegistrationDate = new DateTime(2026, 1, 10) },
                new Client { Id = 2, FullName = "Олена Коваль", Email = "olena@example.com", Phone = "+380672223344", RegistrationDate = new DateTime(2026, 2, 15) },
                new Client { Id = 3, FullName = "Максим Сидоренко", Email = "maksim@example.com", Phone = "+380933334455", RegistrationDate = new DateTime(2026, 3, 20) }
            );
        }
    }
}
