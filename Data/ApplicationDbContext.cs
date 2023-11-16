using Microsoft.EntityFrameworkCore;
using ProductManagerAPI.Data.Entities;

namespace ProductManagerAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}
