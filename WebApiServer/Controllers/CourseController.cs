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
        public async Task<IActionResult> Post([FromForm] CourseDto value)
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

        // PUT api/<CourseController>/5
        [HttpPut("{id}")]
        async public Task<IActionResult> Put(int id, [FromForm] CourseDto value)
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
                    await value.FileImage.CopyToAsync(fs); // שימוש ב-Async
                }
                var urlForClient = "/Images/" + fileName;
            }
            return await service.UpdateItem(id, value);
        }

        // DELETE api/<CourseController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.DeleteItem(id);
        }
    }
}
