using App.Domain.Entity.blk;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class WhiteListConfiguration : IEntityTypeConfiguration<WhiteList>
    {
        public void Configure(EntityTypeBuilder<WhiteList> entity)
        {
            entity.ToTable("WhiteList", "blk");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.BlackListId).HasColumnName("BlackListID");

            entity.Property(e => e.Comments)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.RequestedById).HasColumnName("RequestedByID");

            entity.Property(e => e.WhiteListDate).HasColumnType("date");

            entity.HasOne(d => d.BlackList)
                .WithMany(p => p.WhiteList)
                .HasForeignKey(d => d.BlackListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("whitelist_fk");

            entity.HasOne(d => d.RequestedBy)
                .WithMany()
                .HasForeignKey(d => d.RequestedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("whitelist_fk_1");
        }
    }
}
