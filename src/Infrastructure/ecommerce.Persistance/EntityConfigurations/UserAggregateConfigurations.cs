using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce.Persistance.EntityConfigurations;
internal sealed class UserAggregateConfigurations : IEntityTypeConfiguration<UserAggregate> {
    public void Configure(EntityTypeBuilder<UserAggregate> builder) {
        builder.ToTable("Users", "user");
        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .HasConversion(id => id.Value, value => UserId.Create(value));

        builder.OwnsOne(user => user.FullName, fullName => {
            fullName.Property(x => x.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(50);
            fullName.Property(x => x.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(50);
        });

        builder.OwnsOne(user => user.Email, email => {
            email.Property(x => x.Value).HasColumnName("Email").IsRequired().HasMaxLength(100);
            email.Property(x => x.IsConfirmed).HasColumnName("EmailConfirmed");
        });

        builder.OwnsOne(user => user.PhoneNumber, phoneNumber => {
            phoneNumber.Property(x => x.Value).HasColumnName("PhoneNumber").HasMaxLength(20);
            phoneNumber.Property(x => x.IsConfirmed).HasColumnName("PhoneNumberConfirmed");
        });

        builder.OwnsOne(user => user.Password, password => {
            password.Property(x => x.HashedValue).HasColumnName("PasswordHash").IsRequired();
            password.Property(p => p.Salt).HasColumnName("PasswordSalt").IsRequired();
        });

        builder.OwnsMany(user => user.RefreshTokens, refreshToken => {
            refreshToken.WithOwner().HasForeignKey("UserId");
            refreshToken.ToTable("RefreshTokens", "user");
            refreshToken.HasKey(x => x.Token);
            refreshToken.Property(x => x.Token).HasMaxLength(200).IsRequired();
            refreshToken.Property(x => x.Expires).IsRequired();
            refreshToken.Property(x => x.Created).IsRequired();
            refreshToken.Property(x => x.CreatedByIp).HasMaxLength(100).IsRequired();
            builder.Metadata.FindNavigation(nameof(UserAggregate.RefreshTokens))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.Metadata.FindNavigation(nameof(UserAggregate.Email))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(UserAggregate.PhoneNumber))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(UserAggregate.Password))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}