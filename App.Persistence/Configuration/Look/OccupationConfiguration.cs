using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class OccupationConfiguration : IEntityTypeConfiguration<Occupation>
    {
        public void Configure(EntityTypeBuilder<Occupation> entity)
        {
            entity.ToTable("Occupation", "look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

            entity.Property(e => e.Title)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.TitleEn)
                .IsRequired()
                .HasColumnName("TitleEN")
                .HasColumnType("character varying");

            entity.HasOne(d => d.Organization)
                .WithMany()
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("occupation_fk");
        }
    }
}
