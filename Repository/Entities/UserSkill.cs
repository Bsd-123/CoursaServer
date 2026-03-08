using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class UserSkill
{
    public int UserId { get; set; }

    public int SkillId { get; set; }

    public DateTime? AcquiredDate { get; set; }

    public int? ProficiencyLevel { get; set; }

    public virtual Skill Skill { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
