using Clean.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Persistence.Configuration.Look
{
    public class ScreenConfiguration : IEntityTypeConfiguration<Screen>
    {
        public void Configure(EntityTypeBuilder<Screen> entity)
        {
            entity.ToTable("Screen", "look");

            entity.HasIndex(e => e.ModuleId);

            entity.HasIndex(e => e.ParentId);

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(e => e.DirectoryPath)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(e => e.Icon)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");

            entity.Property(e => e.ParentId).HasColumnName("ParentID");

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasOne(d => d.Module)
                .WithMany(p => p.Screen)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("screen_fk");

            entity.HasOne(d => d.Parent)
                .WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("screen_parent_fk");

        }
    }
}
