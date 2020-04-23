using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class MaritalStatusConfiguration : IEntityTypeConfiguration<MaritalStatus>
    {
        public void Configure(EntityTypeBuilder<MaritalStatus> entity)
        {
            entity.ToTable("MaritalStatus", "look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Name).HasMaxLength(100);
        }
    }
}
