
using Clean.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> entity)
        {
            entity.ToTable("Country", "look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Code)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.Title)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.TitleEn)
                    .IsRequired()
                    .HasColumnName("TitleEN")
                    .HasColumnType("character varying");
        }
    }
}
