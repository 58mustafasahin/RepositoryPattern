using GalleryApp.Entity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GalleryApp.DataAccess.EF.Concrete.Configuration
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Subject).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Message).HasMaxLength(250).IsRequired();
            builder.Property(p => p.PhoneNumber).HasMaxLength(11).IsRequired();
        }
    }
}
