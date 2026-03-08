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

    public int? CouponOwnerId { get; set; }

    public string PaymentNumber { get; set; } = null!;

    public int? ReceptionNumber { get; set; }

    public int Id { get; set; }

    public int? CouponManagerId { get; set; }

    public virtual Coupon? CouponManager { get; set; }

    public virtual Coupon? CouponOwner { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
