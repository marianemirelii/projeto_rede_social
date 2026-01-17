using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeSocial.Domain.Models;

namespace RedeSocial.Infrastructure.Data.Maps;

public class CommentMap : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        builder.Property(c => c.Content).IsRequired();

        builder.Property(c => c.CreatedAt).IsRequired();

        builder.HasOne(c => c.Post).WithMany(p => p.Comments).HasForeignKey(c => c.PostId);

        builder.HasOne(c => c.User).WithMany(u => u.Comments).HasForeignKey(c => c.UserId);
    }
}
