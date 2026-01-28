using Microsoft.AspNetCore.Mvc;
using Service.Dto;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly IService<SkillDto> service;
        public SkillController(IService<SkillDto> service)
        {
            this.service = service;
        }
        // GET: api/<SkillController>
        [HttpGet]
        public List<SkillDto> Get()
        {
            return service.GetAll();
        }

        // GET api/<SkillController>/5
        [HttpGet("{id}")]
        public SkillDto Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<SkillController>
        [HttpPost]
        public void Post([FromBody] SkillDto value)
        {
            service.AddItem(value);
        }

        // PUT api/<SkillController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] SkillDto value)
        {
            service.UpdateItem(id, value);
        }

        // DELETE api/<SkillController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.DeleteItem(id);
        }
    }
}
