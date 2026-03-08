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

        public int? CouponOwnerId { get; set; }

        public string PaymentNumber { get; set; } = null!;

        public int? ReceptionNumber { get; set; }

        public int Id { get; set; }

        public int? CouponManagerId { get; set; }

        public CouponDto? CouponManager { get; set; }

        public CouponDto? CouponOwner { get; set; }

        public CourseDto Course { get; set; } = null!;

        public UserDto User { get; set; } = null!;
    }
}
