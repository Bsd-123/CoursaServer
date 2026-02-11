using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Enrollment
{
    public int UserId { get; set; }

    public int CourseId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool? Status { get; set; }

    public double FullPrice { get; set; }

    public int? CouponId { get; set; }

    public int PaymentNumber { get; set; }

    public int? ReceptionNumber { get; set; }

    public virtual Coupon? Coupon { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
