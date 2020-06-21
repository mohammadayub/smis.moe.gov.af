﻿using Clean.Common.Service;
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
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        #region AuditionSetting

        //   private CurrentUser currentUser;
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken),bool track = true)

        {
            if (track)
            {

                List<AuditEntry> AuditEntries = new List<AuditEntry>();
                var User = await UserManager.GetUserAsync(ContextHelper.Current.User);
                //var User = await UserManager.FindByNameAsync(ContextHelper.Current.User.Identity.Name);
                AuditEntries = OnBeforeSaveChanges(User.Id);
                var result = await base.SaveChangesAsync(cancellationToken);
                await OnAfterSaveChanges(AuditEntries);
                return result;
            }
            else
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
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

            return base.SaveChangesAsync();
        }

        #endregion AuditionSettig

    }
}
