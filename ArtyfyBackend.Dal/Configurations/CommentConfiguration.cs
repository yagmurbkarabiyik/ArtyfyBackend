using ArtyfyBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtyfyBackend.Dal.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd();
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.Content).IsRequired().HasMaxLength(500);

        }
    }
}