using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Interfaces;
using Service.Dto;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly IService<CouponDto> service;
        public CouponController(IService<CouponDto> service)
        {
            this.service = service;
        }
        // GET: api/<CouponController>
        [HttpGet]
		[Authorize]
		public async Task<List<CouponDto>> Get()
        {
            return await service.GetAll();
        }

        // GET api/<CouponController>/5
        [HttpGet("{id}")]
		[Authorize]
		public async Task<CouponDto> Get(int id)
        {
            return await service.GetById(id);
        }

        // POST api/<CouponController>
        [HttpPost]
		[Authorize(Roles = "owner,admin")]
		public async Task<CouponDto> Post([FromBody] CouponDto value)
        {
            return await  service.AddItem(value);
        }

        // PUT api/<CouponController>/5
        [HttpPut("{id}")]
		[Authorize(Roles = "owner,admin")]
		public async Task Put(int id, [FromBody] CouponDto value)
        {
             await service.UpdateItem(id, value);
        }

        // DELETE api/<CouponController>/5
        [HttpDelete("{id}")]
		[Authorize(Roles = "admin")]
		public async Task Delete(int id)
        {
            await service.DeleteItem(id);
        }
    }
}
