using System;
using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class ContentType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? DisplayIcon { get; set; }

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
