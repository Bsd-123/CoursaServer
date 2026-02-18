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
    public class CouponService :IService<CouponDto>
    {
        private readonly IRepository<Coupon> repository;
        private readonly IMapper mapper;
        public CouponService(IRepository<Coupon> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public CouponDto AddItem(CouponDto item)
        {
            return mapper.Map<Coupon, CouponDto>(repository.AddItem(mapper.Map<CouponDto, Coupon>(item)));
        }

        public void DeleteItem(int id)
        {
            repository.DeleteItem(id);
        }

        public List<CouponDto> GetAll()
        {
            return mapper.Map<List<Coupon>, List<CouponDto>>(repository.GetAll());
        }

        public CouponDto GetById(int id)
        {
            return mapper.Map<Coupon, CouponDto>(repository.GetById(id));
        }

        public void UpdateItem(int id, CouponDto item)
        {
            repository.UpdateItem(id, mapper.Map<CouponDto, Coupon>(item));
        }
    }
}
