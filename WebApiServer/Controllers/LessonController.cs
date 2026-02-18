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
    public class LessonController : ControllerBase
    {
        private readonly IRepository<Lesson> service;
        public LessonController(IRepository<Lesson> service)
        {
            this.service = service;
        }
        // GET: api/<LessonController>
        [HttpGet]
        public List<Lesson> Get()
        {
            return service.GetAll();
        }

        // GET api/<LessonController>/5
        [HttpGet("{id}")]
        public Lesson Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<LessonController>
        [HttpPost]
        public Lesson Post([FromForm] Lesson value)
        {
            return service.AddItem(value);
        }

        // PUT api/<LessonController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromForm] Lesson value)
        {
            service.UpdateItem(id, value);
        }

        // DELETE api/<LessonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.DeleteItem(id);
        }
    }
}
