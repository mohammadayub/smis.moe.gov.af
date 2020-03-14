using Clean.Common.Service;
using Clean.Domain.Entity.au;
using Clean.Domain.Entity.doc;
using Clean.Domain.Entity.look;
using Clean.Domain.Entity.prc;
using Clean.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Persistence.Context
{
    public abstract class BaseContext : DbContext
    {
        private UserManager<AppUser> UserManager { get; set; }
        public BaseContext(DbContextOptions options,UserManager<AppUser> manager):base(options)
        {
            UserManager = manager;
        }

        
        #region Audit Db Sets
        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<OperationType> OperationTypes { get; set; }
        #endregion

        #region ProcessTracking Section
        public virtual DbSet<Screen> Screens { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Process> Process { get; set; }
        public virtual DbSet<ProcessConnection> ProcessConnection { get; set; }
        public virtual DbSet<ProcessTracking> ProcessTracking { get; set; }
        #endregion

        public virtual DbSet<ScreenDocument> ScreenDocuments { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<Documents> Documents { get; set; }

        public virtual DbSet<SystemStatus> SystemStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audit>(entity =>
            {
                entity.ToTable("Audit", "au");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DbContextObject).HasMaxLength(100);

                entity.Property(e => e.DbObjectName).HasMaxLength(100);

                entity.Property(e => e.OperationTypeId).HasColumnName("OperationTypeID");

                entity.Property(e => e.RecordId)
                    .HasColumnName("RecordID")
                    .HasMaxLength(200);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ValueAfter).HasColumnType("character varying");

                entity.Property(e => e.ValueBefore).HasColumnType("character varying");

                entity.HasOne(d => d.OperationType)
                    .WithMany(p => p.Audit)
                    .HasForeignKey(d => d.OperationTypeId)
                    .HasConstraintName("audit_fk");
            });

            modelBuilder.Entity<OperationType>(entity =>
            {
                entity.ToTable("OperationType", "au");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OperationTypeName).HasColumnType("character varying");
            });

            modelBuilder.Entity<Process>(entity =>
            {
                entity.ToTable("Process", "prc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.ScreenId).HasColumnName("ScreenID");

                entity.Property(e => e.Sorter).HasMaxLength(10);

                entity.HasOne(d => d.Screen)
                    .WithMany(p => p.Process)
                    .HasForeignKey(d => d.ScreenId)
                    .HasConstraintName("_Process__FK");
            });

            modelBuilder.Entity<ProcessConnection>(entity =>
            {
                entity.ToTable("ProcessConnection", "prc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ProcessId).HasColumnName("ProcessID");

                entity.HasOne(d => d.ConnectedToNavigation)
                    .WithMany(p => p.ProcessConnectionConnectedToNavigation)
                    .HasForeignKey(d => d.ConnectedTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_ProcessConnection__FK_1");

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.ProcessConnectionProcess)
                    .HasForeignKey(d => d.ProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_ProcessConnection__FK");
            });

            modelBuilder.Entity<ProcessTracking>(entity =>
            {
                entity.ToTable("ProcessTracking", "prc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

                entity.Property(e => e.ModuleId).HasColumnName("ModuleID");

                entity.Property(e => e.ProcessId).HasColumnName("ProcessID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.ReferedProcessId).HasColumnName("ReferedProcessID");

                entity.Property(e => e.Remarks).HasMaxLength(1000);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.ToUserId)
                    .HasColumnName("ToUserID")
                    .HasColumnType("character varying");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("UserID")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.ProcessTracking)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_ProcessTracking__FK_2");

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.ProcessTrackingProcess)
                    .HasForeignKey(d => d.ProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_ProcessTracking__FK");

                entity.HasOne(d => d.ReferedProcess)
                    .WithMany(p => p.ProcessTrackingReferedProcess)
                    .HasForeignKey(d => d.ReferedProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_ProcessTracking__FK_1");
            });

            modelBuilder.Entity<Screen>(entity =>
            {
                entity.ToTable("Screen", "Look");

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
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable("Module", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<SystemStatus>(entity =>
            {
                entity.ToTable("SystemStatus", "Look");

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
            });

            modelBuilder.Entity<ScreenDocument>(entity =>
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
                    .WithMany(p => p.ScreenDocument)
                    .HasForeignKey(d => d.ScreenId)
                    .HasConstraintName("_ScreenDocument__FK_1");
            });
            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.ToTable("DocumentType", "doc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Category).HasColumnType("character varying");

                entity.Property(e => e.Description).HasColumnType("character varying");

                entity.Property(e => e.Name).HasColumnType("character varying");
            });

            modelBuilder.Entity<Documents>(entity =>
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
            });


            base.OnModelCreating(modelBuilder);
        }

        #region AuditionSetting

        //   private CurrentUser currentUser;
        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            List<AuditEntry> AuditEntries = new List<AuditEntry>();
            var User = await UserManager.FindByNameAsync(ContextHelper.Current.User.Identity.Name);
            AuditEntries = OnBeforeSaveChanges(User.Id);
            var result = await base.SaveChangesAsync(cancellationToken);
            await OnAfterSaveChanges(AuditEntries);
            return result;
        }


        private List<AuditEntry> OnBeforeSaveChanges(int UserId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = String.Concat( entry.Metadata.GetSchema() ,".", entry.Metadata.GetTableName());
                auditEntry.UserId = UserId;
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        // value will be generated by the database, get the value after saving
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            auditEntry.OperationTypeId = 1;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.OperationTypeId = 3;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                                auditEntry.OperationTypeId = 2;

                            }
                            break;
                    }
                }
            }

            // Save audit entities that have all the modifications
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                Audits.Add(auditEntry.ToAudit());
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }


        private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                // Save the Audit entry
                Audits.Add(auditEntry.ToAudit());
            }

            return SaveChangesAsync();
        }

        #endregion AuditionSettig

    }
}
