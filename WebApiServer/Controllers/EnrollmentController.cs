using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Interfaces;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IRepositoryDouble<Enrollment> service;
        public EnrollmentController(IRepositoryDouble<Enrollment> service)
        {
            this.service = service;
        }
        // GET: api/<EnrollmentController>
        [HttpGet]
        public List<Enrollment> Get()
        {
            return service.GetAll();
        }

        // GET api/<EnrollmentController>/5
        [HttpGet("{id}")]
        public Enrollment Get(int id1, int id2)
        {
            return service.GetById(id1, id2);
        }

        // POST api/<EnrollmentController>
        [HttpPost]
        public Enrollment Post([FromForm] Enrollment value)
        {
            return service.AddItem(value);
        }

        // PUT api/<EnrollmentController>/5
        [HttpPut("{id}")]
        public void Put(int id1, int id2, [FromForm] Enrollment value)
        {
            service.UpdateItem(id1, id2, value);
        }

        // DELETE api/<EnrollmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id1, int id2)
        {
            service.DeleteItem(id1, id2);
        }
    }
}
