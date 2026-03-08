using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class UserLogin
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime? LoginDate { get; set; }

    public string? Ipaddress { get; set; }

    public string? DeviceType { get; set; }

    public virtual User User { get; set; } = null!;
}
