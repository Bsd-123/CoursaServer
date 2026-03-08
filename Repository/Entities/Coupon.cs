using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Coupon
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsPercentages { get; set; }

    public double Value { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public double MinPrice { get; set; }

    public bool? Status { get; set; }

    public int? UserId { get; set; }

    public string? RequirementsJson { get; set; }

    public string? Description { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Enrollment> EnrollmentCouponManagers { get; set; } = new List<Enrollment>();

    public virtual ICollection<Enrollment> EnrollmentCouponOwners { get; set; } = new List<Enrollment>();

    public virtual User? User { get; set; }
}
