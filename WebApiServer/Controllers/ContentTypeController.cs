using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Dto;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentTypeController : ControllerBase
    {
        private readonly IService<ContentTypeDto> service;
        public ContentTypeController(IService<ContentTypeDto> service)
        {
            this.service = service;
        }
        // GET: api/<ContentTypeController>
        [HttpGet]
        public List<ContentTypeDto> Get()
        {
            return service.GetAll();
        }

        // GET api/<ContentTypeController>/5
        [HttpGet("{id}")]
        public ContentTypeDto Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<ContentTypeController>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ContentTypeDto Post([FromForm] ContentTypeDto value)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Images/", value.FileImage.FileName);
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                value.FileImage.CopyTo(fs);
                fs.Close();
            }
            return service.AddItem(value);
        }

        // PUT api/<ContentTypeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ContentTypeDto value)
        {
            service.UpdateItem(id,value);
        }

        // DELETE api/<ContentTypeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.DeleteItem(id);
        }
    }
}
