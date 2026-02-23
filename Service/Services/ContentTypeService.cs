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
    public class ContentTypeService : IService<ContentTypeDto>
    {
        private readonly IRepository<ContentType> repository;
        private readonly IMapper mapper;
        public ContentTypeService(IRepository<ContentType> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ContentTypeDto> AddItem(ContentTypeDto item)
        {
            return mapper.Map<ContentType, ContentTypeDto>(await repository.AddItem(mapper.Map<ContentTypeDto, ContentType>(item)));
        }

        public async Task DeleteItem(int id)
        {
           await repository.DeleteItem(id);
        }

        public async Task<List<ContentTypeDto>> GetAll()
        {
            return mapper.Map<List<ContentType>, List<ContentTypeDto>>(await repository.GetAll());
        }

        public async Task<ContentTypeDto> GetById(int id)
        {
            return mapper.Map<ContentType, ContentTypeDto>(await repository.GetById(id));
        }

        public async Task UpdateItem(int id, ContentTypeDto item)
        {
            await repository.UpdateItem(id, mapper.Map<ContentTypeDto, ContentType>(item));
        }
    }
}
