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
        public List<CouponDto> Get()
        {
            return service.GetAll();
        }

        // GET api/<CouponController>/5
        [HttpGet("{id}")]
        public CouponDto Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<CouponController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CouponDto value)
        {
            return await service.AddItem(value);
        }

        // PUT api/<CouponController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CouponDto value)
        {
            return await service.UpdateItem(id, value);
        }

        // DELETE api/<CouponController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.DeleteItem(id);
        }
    }
}
