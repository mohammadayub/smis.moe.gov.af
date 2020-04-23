using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class CrimeTypeConfiguration : IEntityTypeConfiguration<CrimeType>
    {
        public void Configure(EntityTypeBuilder<CrimeType> entity)
        {
            entity.ToTable("CrimeType", "look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Title)
                .IsRequired()
                .HasColumnType("character varying");
        }
    }
}
