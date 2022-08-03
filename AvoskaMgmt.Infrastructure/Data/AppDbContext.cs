using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CStuffControl.Infrastructure
{
    public class AppDbContext : IdentityDbContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // Database.EnsureDeleted();   // удаляем бд со старой схемой
            //Database.EnsureCreated();
        }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<StatusOrder> StatusOrder { get; set; }
        public DbSet<Status> Status { get; set; }

        /* 
                public DbSet<Product> Products { get; set; }
                public DbSet<Tag> Tags { get; set; }
                public DbSet<Feature> Features { get; set; }

                public DbSet<Order> Orders { get; set; }
         */
        /*   public DbSet<OrderProduct> OrderProducts { get; set; } */
        /*  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Userid=postgres;Password=1234;Pooling=false;MinPoolSize=1;MaxPoolSize=20;Timeout=15;SslMode=Disable;Database=postgres;");
    */




    }
}