using Microsoft.AspNetCore.Mvc;
using Service.Dto;
using Service.Interfaces;
using Repository.Entities;
using Repository.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private readonly IServiceDouble<ProgressDto> service;
        public ProgressController(IServiceDouble<ProgressDto> service)
        {
            this.service = service;
        }
        // GET: api/<SkillController>
        [HttpGet]
        public List<ProgressDto> Get()
        {
            return service.GetAll();
        }

        // GET api/<SkillController>/5
        [HttpGet("{id}")]
        public ProgressDto Get(int id1, int id2)
        {
            return service.GetById(id1, id2);
        }

        // POST api/<SkillController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProgressDto value)
        {
            return await service.AddItem(value);
        }

        // PUT api/<SkillController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id1, int id2, [FromForm] ProgressDto value)
        {
            return await service.UpdateItem(id1, id2, value);
        }

        // DELETE api/<SkillController>/5
        [HttpDelete("{id}")]
        public void Delete(int id1, int id2)
        {
            service.DeleteItem(id1, id2);
        }
    }
}
