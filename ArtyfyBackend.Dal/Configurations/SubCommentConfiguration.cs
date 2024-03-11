using ArtyfyBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtyfyBackend.Dal.Configurations
{
    public class SubCommentConfiguration : IEntityTypeConfiguration<SubComment>
    {
        public void Configure(EntityTypeBuilder<SubComment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd();
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.CommentId).IsRequired();
            builder.Property(p => p.Content).IsRequired().HasMaxLength(500);
            builder.HasOne(p => p.Comment).WithMany(p => p.SubComments).HasForeignKey(p => p.CommentId);
        }
    }
}
