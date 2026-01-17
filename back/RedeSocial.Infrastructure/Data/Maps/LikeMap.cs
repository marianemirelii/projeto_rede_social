using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeSocial.Domain.Models;

namespace RedeSocial.Infrastructure.Data.Maps;

public class LikeMap : IEntityTypeConfiguration<Like> 
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Id).ValueGeneratedOnAdd();

        builder.Property(l => l.CreatedAt).IsRequired();

        builder.HasOne(l => l.Post).WithMany(p => p.Likes).HasForeignKey(l => l.PostId);

        builder.HasOne(l => l.User).WithMany(u => u.Likes).HasForeignKey(l => l.UserId);

        builder.HasIndex(l => new { l.PostId, l.UserId }).IsUnique();

    }
}
