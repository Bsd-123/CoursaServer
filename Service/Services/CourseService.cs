using AutoMapper;
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
        public CourseDto AddItem(CourseDto item)
        {
            return mapper.Map<Course, CourseDto>(repository.AddItem(mapper.Map<CourseDto, Course>(item)));
        }

        public void DeleteItem(int id)
        {
            repository.DeleteItem(id);
        }

        public List<CourseDto> GetAll()
        {
            return mapper.Map<List<Course>, List<CourseDto>>(repository.GetAll());
        }

        public CourseDto GetById(int id)
        {
            return mapper.Map<Course, CourseDto>(repository.GetById(id));
        }

        public void UpdateItem(int id, CourseDto item)
        {
            repository.UpdateItem(id, mapper.Map<CourseDto, Course>(item));
        }
    }
}
