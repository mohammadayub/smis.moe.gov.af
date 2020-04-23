using Clean.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Persistence.Configuration.Look
{
    public class SystemStatusConfiguration : IEntityTypeConfiguration<SystemStatus>
    {
        public void Configure(EntityTypeBuilder<SystemStatus> entity)
        {
            entity.ToTable("SystemStatus", "look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(e => e.Sorter)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.StatusType)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.TypeId).HasColumnName("TypeID");
        }
    }
}
