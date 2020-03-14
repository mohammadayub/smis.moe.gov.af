using Clean.Domain.Entity.look;
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
                optionsBuilder.UseNpgsql("Server=localhost; Database=MPRS; Username=postgres; Password=qwer");
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
                entity.ToTable("Country", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            builder.Entity<Province>(entity =>
            {
                entity.ToTable("Province", "Look");

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
                entity.ToTable("Currency", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            builder.Entity<Office>(entity =>
            {
                entity.ToTable("Office", "Look");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);
            });



            builder.Entity<Module>(entity =>
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

            builder.Entity<Screen>(entity =>
            {
                entity.ToTable("Screen", "Look");

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
                entity.ToTable("RoleScreen", "Look");

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
                ent.ToTable("Organization", "Look");
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
            });
        }
    }
}
