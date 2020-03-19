using Clean.Domain.Entity.doc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Persistence.Configuration.Doc
{
    public class ScreenDocumentConfiguration : IEntityTypeConfiguration<ScreenDocument>
    {
        public void Configure(EntityTypeBuilder<ScreenDocument> entity)
        {
            entity.ToTable("ScreenDocument", "doc");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

            entity.Property(e => e.ScreenId).HasColumnName("ScreenID");

            entity.HasOne(d => d.DocumentType)
                .WithMany(p => p.ScreenDocument)
                .HasForeignKey(d => d.DocumentTypeId)
                .HasConstraintName("_ScreenDocument__FK");

            entity.HasOne(d => d.Screen)
                .WithMany()
                .HasForeignKey(d => d.ScreenId)
                .HasConstraintName("_ScreenDocument__FK_1");
        }
    }
}
