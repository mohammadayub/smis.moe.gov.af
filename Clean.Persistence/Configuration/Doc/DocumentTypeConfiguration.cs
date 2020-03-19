using Clean.Domain.Entity.doc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Persistence.Configuration.Doc
{
    public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> entity)
        {
            entity.ToTable("DocumentType", "doc");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Category).HasColumnType("character varying");

            entity.Property(e => e.Description).HasColumnType("character varying");

            entity.Property(e => e.Name).HasColumnType("character varying");
        }
    }
}
