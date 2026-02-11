using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class ContentType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? DisplayIcon { get; set; }

    public bool Status { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual User? User { get; set; }
}
