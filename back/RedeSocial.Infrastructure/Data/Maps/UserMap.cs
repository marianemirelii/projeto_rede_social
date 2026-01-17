using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeSocial.Domain.Models;

namespace RedeSocial.Infrastructure.Data.Maps;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).ValueGeneratedOnAdd();

        builder.Property(u => u.Name).IsRequired().HasMaxLength(255);

        builder.Property(u => u.Email).IsRequired().HasMaxLength(255);

        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.NickName).HasMaxLength(50).HasColumnName("Nick_Name");

        builder.Property(u => u.BirthDate).IsRequired().HasColumnName("Birth_Date");

        builder.Property(u => u.Cep).IsRequired().HasMaxLength(8);

        builder.Property(u => u.Image);

        builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(128);

        builder.HasMany(u => u.Posts).WithOne(p => p.User).HasForeignKey(p => p.UserId);

        builder.HasMany(u => u.Likes).WithOne(l => l.User).HasForeignKey(l => l.UserId);

        builder.HasMany(u => u.Comments).WithOne(c => c.User).HasForeignKey(c => c.UserId);

        builder.HasMany(u => u.FriendsRequested).WithOne(f => f.User).HasForeignKey(f => f.UserId);

        builder.HasMany(u => u.FriendsReceived).WithOne(f => f.FriendUser).HasForeignKey(f => f.FriendUserId);
    }
}
