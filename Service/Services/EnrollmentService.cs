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
    public class EnrollmentService : IServiceDouble<EnrollmentDto>
    {
        private readonly IRepositoryDouble<Enrollment> repository;
        private readonly IMapper mapper;
        public EnrollmentService(IRepositoryDouble<Enrollment> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public Task<IActionResult> AddItem(EnrollmentDto item)
        {
            var result = mapper.Map<Enrollment, EnrollmentDto>(repository.AddItem(mapper.Map<EnrollmentDto, Enrollment>(item)));
            return Task.FromResult<IActionResult>(new CreatedAtActionResult(null, null, null, result));
        }

        public void DeleteItem(int id1, int id2)
        {
            repository.DeleteItem(id1, id2);
        }

        public List<EnrollmentDto> GetAll()
        {
            return mapper.Map<List<Enrollment>, List<EnrollmentDto>>(repository.GetAll());
        }

        public EnrollmentDto GetById(int id1, int id2)
        {
            return mapper.Map<Enrollment, EnrollmentDto>(repository.GetById(id1, id2));
        }

        public Task<IActionResult> UpdateItem(int id1, int id2, EnrollmentDto item)
        {
            repository.UpdateItem(id1, id2, mapper.Map<EnrollmentDto, Enrollment>(item));
            return Task.FromResult<IActionResult>(new NoContentResult());
        }
    }
}
