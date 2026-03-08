using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Owner
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string OwnerName { get; set; } = null!;

    public string? Image { get; set; }

    public string PaymentNumber { get; set; } = null!;

    public bool? Status { get; set; }

    public string? Description { get; set; }

    public bool IsVerified { get; set; }

    public string? Headline { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual User User { get; set; } = null!;
}
