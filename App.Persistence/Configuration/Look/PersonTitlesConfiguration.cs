using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class PersonTitlesConfiguration : IEntityTypeConfiguration<PersonTitles>
    {
        public void Configure(EntityTypeBuilder<PersonTitles> entity)
        {
            entity.ToTable("PersonTitles", "look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

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
