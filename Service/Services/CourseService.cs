using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Interfaces;
using Service.Dto;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CourseService : IService<CourseDto>
    {
        private readonly IRepository<Course> repository;
        private readonly IMapper mapper;
        public CourseService(IRepository<Course> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<CourseDto> AddItem(CourseDto item)
        {
            return mapper.Map<Course, CourseDto>(await repository.AddItem(mapper.Map<CourseDto, Course>(item)));
        }

        public async Task DeleteItem(int id)
        {
            await repository.DeleteItem(id);
        }

        public async Task<List<CourseDto>> GetAll()
        {
            return mapper.Map<List<Course>, List<CourseDto>>(await repository.GetAll());
        }

        public async Task<CourseDto> GetById(int id)
        {
            return mapper.Map<Course, CourseDto>(await repository.GetById(id));
        }

        public async Task UpdateItem(int id, CourseDto item)
        {
            await repository.UpdateItem(id,mapper.Map<CourseDto, Course>(item));
        }
    }
}
