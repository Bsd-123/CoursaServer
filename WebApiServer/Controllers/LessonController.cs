using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Interfaces;
using Service.Dto;
using Service.Interfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly IService<LessonDto> service;
        public LessonController(IService<LessonDto> service)
        {
            this.service = service;
        }
        // GET: api/<LessonController>
        [HttpGet]
        public async Task<List<LessonDto>> Get()
        {
            return await service.GetAll();
        }

        // GET api/<LessonController>/5
        [HttpGet("{id}")]
        public async Task<LessonDto> Get(int id)
        {
            return await service.GetById(id);
        }

        // POST api/<LessonController>
        [HttpPost]
        public async Task<LessonDto> Post([FromForm] LessonDto value)
        {
			if (value.File != null)
			{
				var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
				if (!Directory.Exists(folderPath))
					Directory.CreateDirectory(folderPath);

				var fileName = Guid.NewGuid().ToString() + "_" + value.File.FileName;
				var fullPath = Path.Combine(folderPath, fileName);

				using (var fs = new FileStream(fullPath, FileMode.Create))
				{
					await value.File.CopyToAsync(fs);
				}
				var urlForClient = "/Images/" + fileName;
				value.Content = urlForClient;
			}
            value.Course = null;
            value.Type = null;
			return await  service.AddItem(value);
        }

        // PUT api/<LessonController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromForm] LessonDto value)
        {
			if (value.File != null)
			{
				var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
				if (!Directory.Exists(folderPath))
					Directory.CreateDirectory(folderPath);

				var fileName = Guid.NewGuid().ToString() + "_" + value.File.FileName;
				var fullPath = Path.Combine(folderPath, fileName);

				using (var fs = new FileStream(fullPath, FileMode.Create))
				{
					await value.File.CopyToAsync(fs);
				}
				var urlForClient = "/Images/" + fileName;
				value.Content = urlForClient;
			}
			await service.UpdateItem(id, value);
        }

        // DELETE api/<LessonController>/5
        [HttpDelete("{id}")]
		[Authorize(Roles = "admin,owner")]
		public async Task Delete(int id)
        {
			var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
			var lesson = await service.GetById(id);
			if (lesson != null && currentUserRole == "owner")
			{
				if (lesson.Course.OwnerId == currentUserId)
				{
					await service.DeleteItem(id);
				}
			}
			
        }
    }
}
