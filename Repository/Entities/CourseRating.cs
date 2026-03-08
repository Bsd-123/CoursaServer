using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class CourseRating
{
    public int CourseId { get; set; }

    public int UserId { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
