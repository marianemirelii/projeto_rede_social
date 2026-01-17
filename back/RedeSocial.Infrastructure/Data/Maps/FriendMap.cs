using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeSocial.Domain.Models;

namespace RedeSocial.Infrastructure.Data.Maps;

public class FriendMap : IEntityTypeConfiguration<Friend>
{
    public void Configure(EntityTypeBuilder<Friend> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.FriendUserId).IsRequired();

        builder.Property(f => f.CreatedAt).IsRequired();

        builder.HasOne(f => f.User).WithMany(u => u.FriendsRequested).HasForeignKey(f => f.UserId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(f => f.FriendUser).WithMany(u => u.FriendsReceived).HasForeignKey(f => f.FriendUserId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(f => new { f.UserId, f.FriendUserId }).IsUnique();

    }
}
