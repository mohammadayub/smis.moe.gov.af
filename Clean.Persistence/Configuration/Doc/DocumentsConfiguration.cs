using Clean.Domain.Entity.doc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Persistence.Configuration.Doc
{
    public class DocumentsConfiguration : IEntityTypeConfiguration<Documents>
    {
        public void Configure(EntityTypeBuilder<Documents> entity)
        {
            entity.ToTable("Documents", "doc");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.ContentType)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Description).HasMaxLength(1000);

            entity.Property(e => e.DocumentDate).HasColumnType("timestamp with time zone");

            entity.Property(e => e.DocumentNumber).HasMaxLength(100);

            entity.Property(e => e.DocumentSource).HasMaxLength(100);

            entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

            entity.Property(e => e.EncryptionKey).HasMaxLength(500);

            entity.Property(e => e.FileName)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.LastDownloadDate).HasColumnType("timestamp with time zone");

            entity.Property(e => e.ObjectName).HasMaxLength(100);

            entity.Property(e => e.ObjectSchema).HasMaxLength(100);

            entity.Property(e => e.Path)
                .IsRequired()
                .HasMaxLength(1000);

            entity.Property(e => e.RecordId).HasColumnName("RecordID");

            entity.Property(e => e.Root).HasMaxLength(200);

            entity.Property(e => e.ScreenId).HasColumnName("ScreenID");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.Property(e => e.UploadDate).HasColumnType("timestamp with time zone");

            entity.HasOne(d => d.DocumentType)
                .WithMany(p => p.Documents)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_Documents__FK");
        }
    }
}
