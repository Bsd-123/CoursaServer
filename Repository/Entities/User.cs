using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? RegDate { get; set; }

    public string Role { get; set; } = null!;

    public bool? Status { get; set; }

    public string? ResetPasswordToken { get; set; }

    public DateTime? ResetTokenExpires { get; set; }

    public virtual ICollection<ContentType> ContentTypes { get; set; } = new List<ContentType>();

    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Owner> Owners { get; set; } = new List<Owner>();

    public virtual ICollection<Progress> Progresses { get; set; } = new List<Progress>();

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
}
