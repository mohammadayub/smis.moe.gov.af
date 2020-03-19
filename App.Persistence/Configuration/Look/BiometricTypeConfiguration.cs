using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class BiometricTypeConfiguration : IEntityTypeConfiguration<BiometricType>
    {
        public void Configure(EntityTypeBuilder<BiometricType> entity)
        {
            entity.ToTable("BiometricType", "Look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Title)
                .IsRequired()
                .HasColumnType("character varying");
        }
    }
}
