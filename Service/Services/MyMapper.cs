using AutoMapper;
using Repository.Entities;
using Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class MyMapper : Profile
    {
        string path = Directory.GetCurrentDirectory() + "\\images\\";
        public MyMapper()
        {
            CreateMap<ContentType, ContentTypeDto>();
            CreateMap<ContentTypeDto, ContentType>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerDto, Owner>();
            CreateMap<Skill, SkillDto>();
            CreateMap<SkillDto, Skill>();
            CreateMap<Course, CourseDto>();
            CreateMap<CourseDto, Course>();
            CreateMap<CouponDto, Coupon>();
            CreateMap<Coupon, CouponDto>();
            CreateMap<Enrollment, EnrollmentDto>();
            CreateMap<EnrollmentDto, Enrollment>();
            CreateMap<Progress, ProgressDto>();
            CreateMap<ProgressDto, Progress>();
            CreateMap<Lesson, LessonDto>();
            CreateMap<LessonDto, Lesson>();
            
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserForLogin>();
            CreateMap<UserForLogin, User>();
        }
        //public byte[] fromStringToByte(string mypath)
        //{
        //    if (string.IsNullOrEmpty(mypath)) return null;
        //    return File.ReadAllBytes(path + mypath);
        //}
    }
}
