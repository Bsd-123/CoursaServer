using Microsoft.AspNetCore.Authorization;
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
        public async Task<List<CourseDto>> Get()
        {
            return await service.GetAll();
        }

        // GET api/<CourseController>/5
        [HttpGet("{id}")]
        public async Task<CourseDto> Get(int id)
        {
            return await service.GetById(id);
        }

        // POST api/<CourseController>
        [HttpPost]
		[Authorize(Roles = "owner")]
		public async Task<CourseDto> Post([FromForm] CourseDto value)
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
                value.Image = urlForClient;
			}
            value.Owner = null;
            value.Skill = null;
			return await service.AddItem(value);
        }

        // PUT api/<CourseController>/5
        [HttpPut("{id}")]
		[Authorize(Roles = "owner")]
		async public Task Put(int id, [FromForm] CourseDto value)
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
                    await  value.FileImage.CopyToAsync(fs); // שימוש ב-Async
                }
                var urlForClient = "/Images/" + fileName;
                value.Image = urlForClient;
            }
			value.Owner = null;
			value.Skill = null;
			await service.UpdateItem(id, value);
        }

        // DELETE api/<CourseController>/5

        [HttpDelete("{id}")]
		[Authorize(Roles = "owner,admin")]
		public async Task Delete(int id)
        {
            await service.DeleteItem(id);
        }
    }
}
