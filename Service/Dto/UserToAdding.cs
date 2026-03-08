using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class UserToAdding
    {
        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Password { get; set; } = null!;

        public DateTime? RegDate { get; set; }

        public string Role { get; set; } = null!;

        public bool? Status { get; set; }

        public string? ResetPasswordToken { get; set; }

        public DateTime? ResetTokenExpires { get; set; }
    }
}
