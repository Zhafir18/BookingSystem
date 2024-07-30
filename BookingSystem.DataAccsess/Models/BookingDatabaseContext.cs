using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.DataAccsess.Models;

public partial class BookingDatabaseContext : DbContext
{
    public BookingDatabaseContext()
    {
    }

    public BookingDatabaseContext(DbContextOptions<BookingDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MstBooking> MstBookings { get; set; }

    public virtual DbSet<MstLocation> MstLocations { get; set; }

    public virtual DbSet<MstMenu> MstMenus { get; set; }

    public virtual DbSet<MstResource> MstResources { get; set; }

    public virtual DbSet<MstResourceCode> MstResourceCodes { get; set; }

    public virtual DbSet<MstRole> MstRoles { get; set; }

    public virtual DbSet<MstRoleMenu> MstRoleMenus { get; set; }

    public virtual DbSet<MstRoom> MstRooms { get; set; }

    public virtual DbSet<MstUser> MstUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BookingDatabase;Username=postgres;Password=indocyber");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MstBooking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("MstBooking_pkey");

            entity.ToTable("MstBooking");

            entity.Property(e => e.BookingId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("BookingID");
            entity.Property(e => e.BookingCode).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.DeletedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<MstLocation>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("MstLocation_pkey");

            entity.ToTable("MstLocation");

            entity.Property(e => e.LocationId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("LocationID");
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.DeletedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.LocationName).HasMaxLength(255);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp(6) without time zone");
        });

        modelBuilder.Entity<MstMenu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("MstMenu_pkey");

            entity.ToTable("MstMenu");

            entity.Property(e => e.MenuId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("MenuID");
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.DeletedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.MenuName).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp(6) without time zone");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MstMenuCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_createdby");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.MstMenuDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("fk_deletedby");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.MstMenuUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("fk_updatedby");
        });

        modelBuilder.Entity<MstResource>(entity =>
        {
            entity.HasKey(e => e.ResourceId).HasName("Resource_pkey");

            entity.ToTable("MstResource");

            entity.Property(e => e.ResourceId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ResourceID");
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.DeletedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.ResourceIcon).HasMaxLength(255);
            entity.Property(e => e.ResourceName).HasMaxLength(255);
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp(6) without time zone");
        });

        modelBuilder.Entity<MstResourceCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MstResourceCode_pkey");

            entity.ToTable("MstResourceCode");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.DeletedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.ResourceCode).HasMaxLength(255);
            entity.Property(e => e.ResourceId).HasColumnName("ResourceID");
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp(6) without time zone");

            entity.HasOne(d => d.Resource).WithMany(p => p.MstResourceCodes)
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ResourceID");
        });

        modelBuilder.Entity<MstRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("Role_pkey");

            entity.ToTable("MstRole");

            entity.Property(e => e.RoleId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("RoleID");
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.DeletedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.RoleName).HasMaxLength(255);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp(6) without time zone");
        });

        modelBuilder.Entity<MstRoleMenu>(entity =>
        {
            entity.HasKey(e => new { e.Roleid, e.Menuid }).HasName("MstRoleMenu_pkey");

            entity.ToTable("MstRoleMenu");

            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Menuid).HasColumnName("menuid");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.DeletedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp(6) without time zone");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MstRoleMenuCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_createdby");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.MstRoleMenuDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("fk_deletedby");

            entity.HasOne(d => d.Menu).WithMany(p => p.MstRoleMenus)
                .HasForeignKey(d => d.Menuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MstRoleMenu_menuid_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.MstRoleMenus)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MstRoleMenu_roleid_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.MstRoleMenuUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("fk_updatedby");
        });

        modelBuilder.Entity<MstRoom>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("MstRoom_pkey");

            entity.ToTable("MstRoom");

            entity.Property(e => e.RoomId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("RoomID");
            entity.Property(e => e.Color).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.DeletedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.RoomName).HasMaxLength(255);
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp(6) without time zone");

            entity.HasOne(d => d.Location).WithMany(p => p.MstRooms)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RoomLocation");
        });

        modelBuilder.Entity<MstUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("User_pkey");

            entity.ToTable("MstUser");

            entity.Property(e => e.UserId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("UserID");
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.DeletedDate).HasColumnType("timestamp(6) without time zone");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.LoginName).HasMaxLength(255);
            entity.Property(e => e.MiddleName).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(8000);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UpdatedDate).HasColumnType("timestamp(6) without time zone");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_createdby");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.InverseDeletedByNavigation)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("fk_deletedby");

            entity.HasOne(d => d.Role).WithMany(p => p.MstUsers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("RoleID");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.InverseUpdatedByNavigation)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("fk_updatedby");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
