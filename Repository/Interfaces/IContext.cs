
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IContext
    {
        public IEnumerable<ContentType> ContentTypes { get; }
        public IEnumerable<Coupon> Coupons { get; }
        public IEnumerable<Course> Courses { get; }
        public IEnumerable<Enrollment> Enrollments { get; }
        public IEnumerable<Lesson> Lessons { get; }
        public IEnumerable<Owner> Owners { get; }
        public IEnumerable<Progress> Progresses { get; }
        public IEnumerable<Skill> Skills { get; }
        public IEnumerable<User> Users { get; }
        public void save();
    }
}
