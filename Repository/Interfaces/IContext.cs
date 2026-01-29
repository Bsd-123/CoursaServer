
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository.Interfaces
{
    public interface IContext
    {
        public DbSet<ContentType> ContentTypes { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Progress> Progresses { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<User> Users { get; set; }
        public void save();
    }
}
