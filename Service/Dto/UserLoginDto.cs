using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class UserLoginDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime? LoginDate { get; set; } = DateTime.Now;

        public string? Ipaddress { get; set; }

        public string? DeviceType { get; set; }

        public UserDto? User { get; set; } = null!;
    }
}
