using GalleryApp.Entity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GalleryApp.DataAccess.EF.Concrete.Configuration
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Brand).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Model).HasMaxLength(50).IsRequired();
            builder.Property(p => p.ModelYear).HasMaxLength(4).IsRequired();
            builder.Property(p => p.Km).HasMaxLength(20).IsRequired();
            builder.Property(p => p.FuelType).HasMaxLength(20).IsRequired();
            builder.Property(p => p.Desc).HasMaxLength(200).IsRequired();
        }
    }
}
