using Microsoft.AspNetCore.Mvc;
using Service.Dto;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IService<CourseDto> service;
        public CourseController(IService<CourseDto> service)
        {
            this.service = service;
        }
        // GET: api/<CourseController>
        [HttpGet]
        public List<CourseDto> Get()
        {
            return service.GetAll();
        }

        // GET api/<CourseController>/5
        [HttpGet("{id}")]
        public CourseDto Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<CourseController>
        [HttpPost]
        public void Post([FromBody] CourseDto value)
        {
            service.AddItem(value);
        }

        // PUT api/<CourseController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CourseDto value)
        {
            service.UpdateItem(id, value);
        }

        // DELETE api/<CourseController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.DeleteItem(id);
        }
    }
}
