using Microsoft.EntityFrameworkCore;
using Translator.Dotnet.API.Data;

#nullable disable

namespace Translator.Dotnet.API.Data{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // You can configure entities here using Fluent API if needed
        }
    }
}
