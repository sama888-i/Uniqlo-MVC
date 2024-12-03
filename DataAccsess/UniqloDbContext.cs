using Microsoft.EntityFrameworkCore;
using Uniqlo2.Models;

namespace Uniqlo2.DataAccsess
{
    public class UniqloDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<ProductImage > ProductImages { get; set; }
        public UniqloDbContext(DbContextOptions opt) : base(opt) { }
        
            
        

    }
}
