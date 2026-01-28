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
    public class ContentTypeService : IService<ContentTypeDto>
    {
        private readonly IRepository<ContentType> repository;
        private readonly IMapper mapper;
        public ContentTypeService(IRepository<ContentType> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public ContentTypeDto AddItem(ContentTypeDto item)
        {
            return mapper.Map<ContentType, ContentTypeDto>(repository.AddItem(mapper.Map<ContentTypeDto, ContentType>(item)));
        }

        public void DeleteItem(int id)
        {
            repository.DeleteItem(id);
        }

        public List<ContentTypeDto> GetAll()
        {
            return mapper.Map<List<ContentType>, List<ContentTypeDto>>(repository.GetAll());
        }

        public ContentTypeDto GetById(int id)
        {
            return mapper.Map<ContentType, ContentTypeDto>(repository.GetById(id));
        }

        public void UpdateItem(int id, ContentTypeDto item)
        {
            repository.UpdateItem(id, mapper.Map<ContentTypeDto, ContentType>(item));
        }
    }
}
