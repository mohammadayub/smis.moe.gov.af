using Clean.Common;
using Clean.Domain.Entity.look;
using Clean.Domain.Entity.prc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Clean.Persistence.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, int, AppUserClaims, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {

        }

        #region Screen Management
        public virtual DbSet<Screen> Screens { get; set; }
        public virtual DbSet<RoleScreen> RoleScreens { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(AppConfig.IdentityConnectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Country>(entity =>
            {
                entity.ToTable("Country", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.TitleEn)
                    .IsRequired()
                    .HasColumnName("TitleEN")
                    .HasColumnType("character varying");
            });

            builder.Entity<Province>(entity =>
            {
                entity.ToTable("Province", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Province)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("province_fk");
            });

            builder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.TitleEn)
                    .IsRequired()
                    .HasColumnName("TitleEN")
                    .HasColumnType("character varying");
            });

            builder.Entity<Office>(entity =>
            {
                entity.ToTable("Office", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.OfficeTypeId).HasColumnName("OfficeTypeID");
                entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.TitleEn)
                    .IsRequired()
                    .HasColumnName("TitleEN")
                    .HasColumnType("character varying");

            });


            builder.Entity<Process>(entity =>
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

            builder.Entity<ProcessConnection>(entity =>
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

            builder.Entity<ProcessTracking>(entity =>
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

                entity.Property(e => e.ToUserId).HasColumnName("ToUserID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.ProcessTracking)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_ProcessTracking__FK_2");

            });



            builder.Entity<Module>(entity =>
            {
                entity.ToTable("Module", "look");

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

            builder.Entity<Screen>(entity =>
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
            });

            builder.Entity<RoleScreen>(entity =>
            {
                entity.ToTable("RoleScreen", "look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.ScreenId).HasColumnName("ScreenID");

                entity.HasOne(d => d.Screen)
                    .WithMany(p => p.RoleScreen)
                    .HasForeignKey(d => d.ScreenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("_RoleScreen__FK");
            });

            builder.Entity<Organization>(ent =>
            {
                ent.ToTable("Organization", "look");
            });


            builder.Entity<AppUser>(ent =>
            {
                ent.HasOne(e => e.Organization)
                    .WithMany()
                    .HasForeignKey(e => e.OrganizationID);
            });

            builder.Entity<AppUserRole>(ent =>
            {
                ent.HasOne(e => e.Role)
                    .WithMany()
                    .HasForeignKey(e => e.RoleId);

                ent.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId);
            });
        }
    }
}
