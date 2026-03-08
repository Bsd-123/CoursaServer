using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;

namespace DBFirst.Models;

public partial class CoursaDbContext : DbContext ,IContext
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

    public virtual DbSet<CourseRating> CourseRatings { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Progress> Progresses { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<UserSkill> UserSkills { get; set; }
    
    public async Task SaveAsync()
    {
        await SaveChangesAsync();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;database=coursaDB;trusted_connection=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__content___3213E83FB0225CFB");

            entity.ToTable("content_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AllowedExtensions).HasMaxLength(255);
            entity.Property(e => e.DisplayIcon).HasColumnName("displayIcon");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .HasColumnName("displayName");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__coupon__3213E83FE15CCD6E");

            entity.ToTable("coupon");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CourseId).HasColumnName("courseId");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("endDate");
            entity.Property(e => e.MinPrice).HasColumnName("minPrice");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.RequirementsJson).HasColumnName("requirementsJson");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("startDate");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasOne(d => d.Course).WithMany(p => p.Coupons)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__coupon__courseId__398D8EEE");

            entity.HasOne(d => d.User).WithMany(p => p.Coupons)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__coupon__userId__02FC7413");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__course__3213E83F4904665F");

            entity.ToTable("course");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.OwnerId).HasColumnName("ownerId");
            entity.Property(e => e.Percentage).HasColumnName("percentage");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.SkillId).HasColumnName("skillId");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.ValidityDays).HasColumnName("validityDays");

            entity.HasOne(d => d.Owner).WithMany(p => p.Courses)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__course__ownerId__2F10007B");

            entity.HasOne(d => d.Skill).WithMany(p => p.Courses)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__course__skillId__2E1BDC42");
        });

        modelBuilder.Entity<CourseRating>(entity =>
        {
            entity.HasKey(e => new { e.CourseId, e.UserId }).HasName("PK__CourseRa__1855FD6325258D57");

            entity.ToTable("CourseRating");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseRatings)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__CourseRat__Cours__43A1090D");

            entity.HasOne(d => d.User).WithMany(p => p.CourseRatings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__CourseRat__UserI__44952D46");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.ToTable("enrollment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CouponManagerId).HasColumnName("couponManagerId");
            entity.Property(e => e.CouponOwnerId).HasColumnName("couponOwnerId");
            entity.Property(e => e.CourseId).HasColumnName("courseId");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("endDate");
            entity.Property(e => e.FullPrice).HasColumnName("fullPrice");
            entity.Property(e => e.PaymentNumber).HasMaxLength(100);
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
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.CouponManager).WithMany(p => p.EnrollmentCouponManagers)
                .HasForeignKey(d => d.CouponManagerId)
                .HasConstraintName("FK__enrollmen__couponManagerId");

            entity.HasOne(d => d.CouponOwner).WithMany(p => p.EnrollmentCouponOwners)
                .HasForeignKey(d => d.CouponOwnerId)
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
            entity.Property(e => e.DurationSec).HasColumnName("durationSec");
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
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Headline).HasMaxLength(150);
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.OwnerName)
                .HasMaxLength(30)
                .HasColumnName("ownerName");
            entity.Property(e => e.PaymentNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
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
            entity.Property(e => e.IsCompleted).HasColumnName("isCompleted");
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
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Skills)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__skill__userId__03F0984C");
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
            entity.Property(e => e.ResetPasswordToken).HasMaxLength(255);
            entity.Property(e => e.ResetTokenExpires).HasColumnType("datetime");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValueSql("(user_name())")
                .HasColumnName("role");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserLogi__3214EC0746C7A5C2");

            entity.Property(e => e.DeviceType).HasMaxLength(100);
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .HasColumnName("IPAddress");
            entity.Property(e => e.LoginDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserLogins_Users");
        });

        modelBuilder.Entity<UserSkill>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.SkillId }).HasName("PK__UserSkil__7A72C55400623B2C");

            entity.ToTable("UserSkill");

            entity.Property(e => e.AcquiredDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProficiencyLevel).HasDefaultValue(1);

            entity.HasOne(d => d.Skill).WithMany(p => p.UserSkills)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSkill__Skill__4A4E069C");

            entity.HasOne(d => d.User).WithMany(p => p.UserSkills)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSkill__UserI__4959E263");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
