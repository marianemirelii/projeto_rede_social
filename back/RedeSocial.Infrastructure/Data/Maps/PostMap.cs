using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeSocial.Domain.Models;

namespace RedeSocial.Infrastructure.Data.Maps;

public class PostMap : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        builder.Property(p => p.Content).IsRequired();

        builder.Property(p => p.IsPublic).IsRequired();

        builder.Property(p => p.Image);

        builder.Property(p => p.CreatedAt).IsRequired();

        builder.HasOne(p => p.User).WithMany(u => u.Posts).HasForeignKey(p => p.UserId);

        builder.HasIndex(p => p.UserId);

        builder.HasMany(p => p.Likes).WithOne(l => l.Post).HasForeignKey(l => l.PostId);

        builder.HasMany(p => p.Comments).WithOne(c => c.Post).HasForeignKey(c => c.PostId);

    }

}