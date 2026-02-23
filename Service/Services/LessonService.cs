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
        public async Task<LessonDto> AddItem(LessonDto item)
        {
            return mapper.Map<Lesson, LessonDto>(await repository.AddItem(mapper.Map<LessonDto, Lesson>(item)));
        }

        public async Task DeleteItem(int id)
        {
            await repository.DeleteItem(id);
        }

        public async Task<List<LessonDto>> GetAll()
        {
            return mapper.Map<List<Lesson>, List<LessonDto>>(await repository.GetAll());
        }

        public async Task<LessonDto> GetById(int id)
        {
            return mapper.Map<Lesson, LessonDto>(await repository.GetById(id));
        }

        public async Task UpdateItem(int id, LessonDto item)
        {
            await repository.UpdateItem(id,mapper.Map<LessonDto, Lesson>(item));
        }
    }
}