using System;
using System.Collections.Generic;

namespace DBFirst.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? RegDate { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Owner> Owners { get; set; } = new List<Owner>();

    public virtual ICollection<Progress> Progresses { get; set; } = new List<Progress>();
}
