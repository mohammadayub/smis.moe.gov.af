using App.Domain.Entity.blk;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class BlackListConfiguration : IEntityTypeConfiguration<BlackList>
    {
        public void Configure(EntityTypeBuilder<BlackList> entity)
        {
            entity.ToTable("BlackList", "blk");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.BlackListDate).HasColumnType("date");

            entity.Property(e => e.BlackListProfileId).HasColumnName("BlackListProfileID");

            entity.Property(e => e.BlackListReasonId).HasColumnName("BlackListReasonID");

            entity.Property(e => e.Comments)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.PassportNumber)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.RequestedById).HasColumnName("RequestedByID");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.BlackListProfile)
                .WithMany(p => p.BlackList)
                .HasForeignKey(d => d.BlackListProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blacklist_fk_1");

            entity.HasOne(d => d.BlackListReason)
                .WithMany(p => p.BlackList)
                .HasForeignKey(d => d.BlackListReasonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blacklist_fk");

            entity.HasOne(d => d.RequestedBy)
                .WithMany()
                .HasForeignKey(d => d.RequestedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blacklist_fk_2");
        }
    }
}
