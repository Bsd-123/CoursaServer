using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly IRepository<Coupon> service;
        public CouponController(IRepository<Coupon> service)
        {
            this.service = service;
        }
        // GET: api/<CouponController>
        [HttpGet]
        public List<Coupon> Get()
        {
            return service.GetAll();
        }

        // GET api/<CouponController>/5
        [HttpGet("{id}")]
        public Coupon Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<CouponController>
        [HttpPost]
        public Coupon Post([FromBody] Coupon value)
        {
            return service.AddItem(value);
        }

        // PUT api/<CouponController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Coupon value)
        {
            service.UpdateItem(id, value);
        }

        // DELETE api/<CouponController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.DeleteItem(id);
        }
    }
}
