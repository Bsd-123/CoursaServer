using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Dto;
using Service.Interfaces;
using System.Net.Mime;

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
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "owner")]
        public async Task<IActionResult> Post([FromForm] ContentTypeDto value)
        {
            if (value.FileImage != null)
            {
                var path = Path.Combine(Environment.CurrentDirectory, "Images/", value.FileImage.FileName);
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    value.FileImage.CopyTo(fs);
                    fs.Close();
                }
            }

            return await service.AddItem(value);
        }

        // PUT api/<ContentTypeController>/5
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "owner")]
        public async Task<IActionResult> Put(int id, [FromForm] ContentTypeDto value)
        {
            if (value.FileImage != null)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid().ToString() + "_" + value.FileImage.FileName;
                var fullPath = Path.Combine(folderPath, fileName);

                using (var fs = new FileStream(fullPath, FileMode.Create))
                {
                    await value.FileImage.CopyToAsync(fs); 
                }
                var urlForClient = "/Images/" + fileName;
                value.DisplayIcon = urlForClient;
            }
            return await service.UpdateItem(id, value);
        }

        // DELETE api/<ContentTypeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.DeleteItem(id);
        }
    }
}
