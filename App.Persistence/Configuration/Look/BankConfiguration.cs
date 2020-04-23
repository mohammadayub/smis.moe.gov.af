using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class BankConfiguration : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> entity)
        {
            entity.ToTable("Bank", "look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CountryId).HasColumnName("CountryID");

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
