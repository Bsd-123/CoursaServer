using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities;

public partial class Coupon
{
    public int Id { get; set; }
    [ForeignKey("Course")]
    public int CourseId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsPercentages { get; set; }

    public double Value { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public double MinPrice { get; set; }

    public bool? Status { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
