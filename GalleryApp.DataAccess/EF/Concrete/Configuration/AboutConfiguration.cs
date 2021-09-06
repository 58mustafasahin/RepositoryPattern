using GalleryApp.Entity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GalleryApp.DataAccess.EF.Concrete.Configuration
{
    public class AboutConfiguration : IEntityTypeConfiguration<About>
    {
        public void Configure(EntityTypeBuilder<About> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Vision).HasMaxLength(500);
            builder.Property(p => p.Mission).HasMaxLength(500);
            builder.Property(p => p.History).HasMaxLength(500);
        }
    }
}
