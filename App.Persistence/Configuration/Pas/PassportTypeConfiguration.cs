using App.Domain.Entity.pas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Pas
{
    public class PassportTypeConfiguration : IEntityTypeConfiguration<PassportType>
    {
        public void Configure(EntityTypeBuilder<PassportType> entity)
        {
            entity.ToTable("PassportType", "pas");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Code)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("character varying");
        }
    }
}
