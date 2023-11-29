using Microsoft.EntityFrameworkCore;
using MVC_One_To_Many_Relation_with_EF_Core.Models;

namespace MVC_One_To_Many_Relation_with_EF_Core.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) 
        {
                
        }

        public DbSet<Slider> Sliders{ get; set; }
        public DbSet<Tag> Tags{ get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
    }
}
