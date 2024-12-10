using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Uniqlo2.Models;

namespace Uniqlo2.DataAccsess
{
    public class UniqloDbContext : IdentityDbContext <User>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }
        public DbSet<ProductComment> ProductComments{ get; set; }
        public DbSet<Tag> Tags { get; set; }
        public UniqloDbContext(DbContextOptions opt) : base(opt) { }
        
        
            
        

    }
}
