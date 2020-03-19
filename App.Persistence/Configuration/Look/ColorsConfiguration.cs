using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class ColorsConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> entity)
        {
            entity.ToTable("Color", "Look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.ColorType)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.NameEn)
                .IsRequired()
                .HasColumnName("NameEN")
                .HasColumnType("character varying");
        }
    }
}
