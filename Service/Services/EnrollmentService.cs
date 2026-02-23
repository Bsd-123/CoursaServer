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
        public async Task<EnrollmentDto> AddItem(EnrollmentDto item)
        {
            return mapper.Map<Enrollment, EnrollmentDto>(await repository.AddItem(mapper.Map<EnrollmentDto, Enrollment>(item)));
        }

        public async Task DeleteItem(int id1, int id2)
        {
            await repository.DeleteItem(id1, id2);
        }

        public async Task<List<EnrollmentDto>> GetAll()
        {
            return mapper.Map<List<Enrollment>, List<EnrollmentDto>>(await repository.GetAll());
        }

        public async Task<EnrollmentDto> GetById(int id1, int id2)
        {
            return mapper.Map<Enrollment, EnrollmentDto>(await repository.GetById(id1, id2));
        }

        public async Task UpdateItem(int id1, int id2, EnrollmentDto item)
        {
            await repository.UpdateItem(id1, id2, mapper.Map<EnrollmentDto, Enrollment>(item));
        }
    }
}
