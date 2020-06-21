
using Clean.Domain.Entity.doc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> entity)
        {
            entity.ToTable("DocumentType", "look");


            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("nextval('doc.Documents_ID_seq'::regclass)");

            entity.Property(e => e.Category).HasMaxLength(5);

            entity.Property(e => e.Description).HasMaxLength(200);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

          
        }
    }
}
