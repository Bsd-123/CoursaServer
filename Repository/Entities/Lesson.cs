using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Lesson
{
    public int Id { get; set; }

    public int Idx { get; set; }

    public string Name { get; set; } = null!;

    public int TypeId { get; set; }

    public int CourseId { get; set; }

    public string Content { get; set; } = null!;

    public string MimeType { get; set; } = null!;

    public bool IsFree { get; set; }

    public bool? Status { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Progress> Progresses { get; set; } = new List<Progress>();

    public virtual ContentType Type { get; set; } = null!;
}
