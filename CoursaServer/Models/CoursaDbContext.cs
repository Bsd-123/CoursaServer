using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;
namespace DBFirst.Models;

public partial class CoursaDbContext : DbContext
{
    public CoursaDbContext()
    {
    }

    public CoursaDbContext(DbContextOptions<CoursaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ContentType> ContentTypes { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Progress> Progresses { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=coursaDB;trusted_connection=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__content___3213E83FB0225CFB");

            entity.ToTable("content_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DisplayIcon)
                .HasMaxLength(100)
                .HasColumnName("displayIcon");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__coupon__3213E83FE15CCD6E");

            entity.ToTable("coupon");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CourseId).HasColumnName("courseId");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("endDate");
            entity.Property(e => e.MinPrice).HasColumnName("minPrice");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("startDate");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasOne(d => d.Course).WithMany(p => p.Coupons)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__coupon__courseId__398D8EEE");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__course__3213E83F4904665F");

            entity.ToTable("course");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.OwnerId).HasColumnName("ownerId");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.SkillId).HasColumnName("skillId");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");

            entity.HasOne(d => d.Owner).WithMany(p => p.Courses)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__course__ownerId__2F10007B");

            entity.HasOne(d => d.Skill).WithMany(p => p.Courses)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__course__skillId__2E1BDC42");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.CourseId }).HasName("PK__enrollme__C9309802234E19A7");

            entity.ToTable("enrollment");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CourseId).HasColumnName("courseId");
            entity.Property(e => e.CouponId).HasColumnName("couponId");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("endDate");
            entity.Property(e => e.FullPrice).HasColumnName("fullPrice");
            entity.Property(e => e.ReceptionNumber)
                .HasDefaultValue(0)
                .HasColumnName("receptionNumber");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("startDate");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");

            entity.HasOne(d => d.Coupon).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.CouponId)
                .HasConstraintName("FK__enrollmen__coupo__4222D4EF");

            entity.HasOne(d => d.Course).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__enrollmen__cours__3F466844");

            entity.HasOne(d => d.User).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__enrollmen__userI__3E52440B");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__lesson__3213E83F5FB47301");

            entity.ToTable("lesson");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(100)
                .HasColumnName("content");
            entity.Property(e => e.CourseId).HasColumnName("courseId");
            entity.Property(e => e.Idx).HasColumnName("idx");
            entity.Property(e => e.MimeType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mimeType");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.TypeId).HasColumnName("typeId");

            entity.HasOne(d => d.Course).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__lesson__courseId__35BCFE0A");

            entity.HasOne(d => d.Type).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__lesson__typeId__34C8D9D1");
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__owner__3213E83FEB9FA050");

            entity.ToTable("owner");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .HasColumnName("image");
            entity.Property(e => e.OwnerName)
                .HasMaxLength(30)
                .HasColumnName("ownerName");
            entity.Property(e => e.PaymentNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Percentage).HasColumnName("percentage");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Owners)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__owner__userId__286302EC");
        });

        modelBuilder.Entity<Progress>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LessonId }).HasName("PK__progress__5412B58859B9A846");

            entity.ToTable("progress");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.LessonId).HasColumnName("lessonId");
            entity.Property(e => e.LastView)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("lastView");
            entity.Property(e => e.Seconds).HasColumnName("seconds");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Progresses)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__progress__lesson__46E78A0C");

            entity.HasOne(d => d.User).WithMany(p => p.Progresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__progress__userId__45F365D3");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__skill__3213E83F4B858823");

            entity.ToTable("skill");

            entity.HasIndex(e => e.Name, "UQ__skill__72E12F1B1E52B38A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user__3213E83F950B0648");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "UQ__user__AB6E6164BAD9CAC6").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.RegDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("regDate");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
