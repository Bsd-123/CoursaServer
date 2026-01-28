using Microsoft.AspNetCore.Mvc;
using Service.Dto;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IService<OwnerDto> service;
        public OwnerController(IService<OwnerDto> service)
        {
            this.service = service;
        }
        // GET: api/<OwnerController>
        [HttpGet]
        public List<OwnerDto> Get()
        {
            return service.GetAll();
        }

        // GET api/<OwnerController>/5
        [HttpGet("{id}")]
        public OwnerDto Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<OwnerController>
        [HttpPost]
        public void Post([FromBody] OwnerDto value)
        {
            service.AddItem(value);
        }

        // PUT api/<OwnerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] OwnerDto value)
        {
            service.UpdateItem(id, value);
        }

        // DELETE api/<OwnerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.DeleteItem(id);
        }
    }
}
