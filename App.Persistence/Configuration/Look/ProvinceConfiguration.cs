
using Clean.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> entity)
        {
            entity.ToTable("Province", "look");

            entity.HasIndex(e => e.CountryId);

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Code)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");

            entity.Property(e => e.Title)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.TitleEn)
                .IsRequired()
                .HasColumnName("TitleEN")
                .HasColumnType("character varying");

            entity.HasOne(d => d.Country)
                .WithMany(p => p.Province)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("province_fk");
        }
    }
}
