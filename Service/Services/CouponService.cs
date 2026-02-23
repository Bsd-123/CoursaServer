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
    public class CouponService :IService<CouponDto>
    {
        private readonly IRepository<Coupon> repository;
        private readonly IMapper mapper;
        public CouponService(IRepository<Coupon> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<CouponDto> AddItem(CouponDto item)
        {
            return mapper.Map<Coupon, CouponDto>(await repository.AddItem(mapper.Map<CouponDto, Coupon>(item)));
        }

        public async Task DeleteItem(int id)
        {
            await repository.DeleteItem(id);
        }

        public async Task<List<CouponDto>> GetAll()
        {
            return mapper.Map<List<Coupon>, List<CouponDto>>(await repository.GetAll());
        }

        public async Task<CouponDto> GetById(int id)
        {
            return mapper.Map<Coupon, CouponDto>(await repository.GetById(id));
        }

        public async Task UpdateItem(int id, CouponDto item)
        {
            await repository.UpdateItem(id,mapper.Map<CouponDto, Coupon>(item));
        }
    }
}
