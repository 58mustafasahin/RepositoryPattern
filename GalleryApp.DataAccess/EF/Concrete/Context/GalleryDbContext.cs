using GalleryApp.Entity.Entity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GalleryApp.DataAccess.EF.Concrete.Context
{
    public class GalleryDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = Environment.GetEnvironmentVariable("MYSQL_URI");
            //optionsBuilder.UseMySQL(connectionString);
            optionsBuilder.UseSqlServer("Server=.;Database=GalleryApp;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Slider> Sliders { get; set; }

    }
}
