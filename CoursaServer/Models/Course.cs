using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBFirst.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Price { get; set; }

    public string? Image { get; set; }
    [ForeignKey("Skill")]
    public int SkillId { get; set; }
    [ForeignKey("Course")]
    public int OwnerId { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual Owner Owner { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;
}
