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
    public class LessonService: IService<LessonDto>
    {
        private readonly IRepository<Lesson> repository;
        private readonly IMapper mapper;
        public LessonService(IRepository<Lesson> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public Task<IActionResult> AddItem(LessonDto item)
        {
            var result = mapper.Map<Lesson, LessonDto>(repository.AddItem(mapper.Map<LessonDto, Lesson>(item)));
            return Task.FromResult<IActionResult>(new CreatedAtActionResult(null, null, null, result));
        }

        public void DeleteItem(int id)
        {
            repository.DeleteItem(id);
        }

        public List<LessonDto> GetAll()
        {
            return mapper.Map<List<Lesson>, List<LessonDto>>(repository.GetAll());
        }

        public LessonDto GetById(int id)
        {
            return mapper.Map<Lesson, LessonDto>(repository.GetById(id));
        }

        public Task<IActionResult> UpdateItem(int id, LessonDto item)
        {
            repository.UpdateItem(id,mapper.Map<LessonDto, Lesson>(item));
            return Task.FromResult<IActionResult>(new NoContentResult());
        }
    }
}