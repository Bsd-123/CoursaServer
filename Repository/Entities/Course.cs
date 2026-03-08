using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Price { get; set; }

    public string? Image { get; set; }

    public int SkillId { get; set; }

    public int OwnerId { get; set; }

    public bool? Status { get; set; }

    public int ValidityDays { get; set; }

    public double Percentage { get; set; }

    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();

    public virtual ICollection<CourseRating> CourseRatings { get; set; } = new List<CourseRating>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual Owner Owner { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;
}
