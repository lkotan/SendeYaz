using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SendeYaz.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SendeYaz.DataAccess.Mappings.EF
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasIndex(x => x.RefreshToken).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.RefreshToken).HasMaxLength(255);
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.PasswordSalt).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
        }
    }
}
