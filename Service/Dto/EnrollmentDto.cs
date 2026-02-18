using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class EnrollmentDto
    {
        public int UserId { get; set; }

        public int CourseId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool? Status { get; set; }

        public double FullPrice { get; set; }

        public int? CouponId { get; set; }

        public int PaymentNumber { get; set; }

        public int? ReceptionNumber { get; set; }

        public CouponDto? Coupon { get; set; }

        public CourseDto? Course { get; set; } = null!;

        public UserDto? User { get; set; } = null!;
    }
}
