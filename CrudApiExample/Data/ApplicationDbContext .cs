using Microsoft.EntityFrameworkCore;
using CrudApiExample.Models;

namespace CrudApiExample.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }//
}
