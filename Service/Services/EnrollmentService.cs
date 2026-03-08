using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Interfaces;
using Service.Dto;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EnrollmentService : IService<EnrollmentDto>
    {
        private readonly IRepository<Enrollment> repository;
        private readonly IMapper mapper;
        public EnrollmentService(IRepository<Enrollment> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<EnrollmentDto> AddItem(EnrollmentDto item)
        {
            return mapper.Map<Enrollment, EnrollmentDto>(await repository.AddItem(mapper.Map<EnrollmentDto, Enrollment>(item)));
        }

        public async Task DeleteItem(int id)
        {
            await repository.DeleteItem(id);
        }

        public async Task<List<EnrollmentDto>> GetAll()
        {
            return mapper.Map<List<Enrollment>, List<EnrollmentDto>>(await repository.GetAll());
        }

        public async Task<EnrollmentDto> GetById(int id)
        {
            return mapper.Map<Enrollment, EnrollmentDto>(await repository.GetById(id));
        }
        public async Task<EnrollmentDto> GetById(int id, int userId, string? userRole)
        {
            // 2. שליפת הנתון מהשירות
            var enrollment = await GetById(id);
            if (enrollment == null) throw new KeyNotFoundException("Enrollment not found");

            if (userRole == "owner" && enrollment.Course.Owner.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not the owner of this course");
            }

            return enrollment;
        }
        public async Task<List<EnrollmentDto>> GetByUserId(int id)
        {
            return mapper.Map<List<Enrollment>, List<EnrollmentDto>>((await repository.GetAll()).Where(e => e.UserId == id).ToList());
        }

        public async Task<List<EnrollmentDto>> GetMineByCourseId(int id, int userId)
        {
            return mapper.Map<List<Enrollment>, List<EnrollmentDto>>((await repository.GetAll()).Where(x => x.CourseId == id && x.UserId == userId && x.EndDate >= DateTime.Now).ToList());
        }

        public async Task UpdateItem(int id, EnrollmentDto item)
        {
            await repository.UpdateItem(id, mapper.Map<EnrollmentDto, Enrollment>(item));
        }
    }
}
