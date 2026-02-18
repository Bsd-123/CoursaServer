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
    public class EnrollmentController : ControllerBase
    {
        private readonly IServiceDouble<EnrollmentDto> service;
        public EnrollmentController(IServiceDouble<EnrollmentDto> service)
        {
            this.service = service;
        }
        // GET: api/<EnrollmentController>
        [HttpGet]
        public List<EnrollmentDto> Get()
        {
            return service.GetAll();
        }

        // GET api/<EnrollmentController>/5
        [HttpGet("{id}")]
        public EnrollmentDto Get(int id1, int id2)
        {
            return service.GetById(id1, id2);
        }

        // POST api/<EnrollmentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EnrollmentDto  value)
        {
            return await service.AddItem(value);
        }

        // PUT api/<EnrollmentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id1, int id2, [FromForm] EnrollmentDto value)
        {
            return await service.UpdateItem(id1, id2, value);
        }

        // DELETE api/<EnrollmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id1, int id2)
        {
            service.DeleteItem(id1, id2);
        }
    }
}
