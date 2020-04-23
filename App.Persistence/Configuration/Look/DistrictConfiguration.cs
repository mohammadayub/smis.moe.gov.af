using Clean.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> entity)
        {
            entity.ToTable("District", "look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

            entity.Property(e => e.Title)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.TitleEn)
                .IsRequired()
                .HasColumnName("TitleEN")
                .HasColumnType("character varying");

            entity.HasOne(d => d.Province)
                .WithMany()
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_District__FK");
        }
    }
}
