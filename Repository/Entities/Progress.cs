using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Progress
{
    public int UserId { get; set; }

    public int LessonId { get; set; }

    public int Seconds { get; set; }

    public DateTime? LastView { get; set; }

    public virtual Lesson Lesson { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
