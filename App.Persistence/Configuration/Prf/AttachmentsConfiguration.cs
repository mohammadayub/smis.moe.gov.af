using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{
    public class AttachmentsConfiguration : IEntityTypeConfiguration<Attachments>
    {
        public void Configure(EntityTypeBuilder<Attachments> entity)
        {
            entity.ToTable("Attachments", "prf");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.AttachmentTypeId).HasColumnName("AttachmentTypeID");

            entity.Property(e => e.ContentType)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.Description).HasColumnType("character varying");

            entity.Property(e => e.DocumentDate).HasColumnType("date");

            entity.Property(e => e.DocumentNumber).HasColumnType("character varying");

            entity.Property(e => e.EncryptionKey).HasColumnType("character varying");

            entity.Property(e => e.Name).HasColumnType("character varying");

            entity.Property(e => e.Path)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.AttachmentType)
                .WithMany(p => p.Attachments)
                .HasForeignKey(d => d.AttachmentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("attachments_fk_1");

            entity.HasOne(d => d.Profile)
                .WithMany(p => p.Attachments)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("attachments_fk");
        }
    }
}
