using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> entity)
        {
            entity.ToTable("Gender", "look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Code)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
