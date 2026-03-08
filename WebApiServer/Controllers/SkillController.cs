using Microsoft.AspNetCore.Authorization;
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
        public async Task<List<SkillDto>> Get()
        {
            return await service.GetAll();
        }

        // GET api/<SkillController>/5
        [HttpGet("{id}")]
        public async Task<SkillDto> Get(int id)
        {
            return await service.GetById(id);
        }

        // POST api/<SkillController>
        [HttpPost]
		[Authorize(Roles = "owner,admin")]
		public async Task<SkillDto> Post([FromForm] SkillDto value)
        {
			if (value.FileImage != null)
			{
                string cleanFileName = System.Text.RegularExpressions.Regex.Replace(value.FileImage.FileName, @"\s+", "_");
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
				if (!Directory.Exists(folderPath))
					Directory.CreateDirectory(folderPath);

				var fileName = Guid.NewGuid().ToString() + "_" + cleanFileName;
				var fullPath = Path.Combine(folderPath, fileName);

				using (var fs = new FileStream(fullPath, FileMode.Create))
				{
					await value.FileImage.CopyToAsync(fs);
				}
				var urlForClient = "/Images/" + fileName;
				value.Image = urlForClient;
			}
			return await service.AddItem(value);
        }

		// PUT api/<SkillController>/5
		[Authorize(Roles = "admin")]
		[HttpPut("{id}")]
        public async Task Put(int id, [FromForm] SkillDto value)
        {
			if (value.FileImage != null)
			{
                string cleanFileName = System.Text.RegularExpressions.Regex.Replace(value.FileImage.FileName, @"\s+", "_");
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
				if (!Directory.Exists(folderPath))
					Directory.CreateDirectory(folderPath);

				var fileName = Guid.NewGuid().ToString() + "_" + cleanFileName;
				var fullPath = Path.Combine(folderPath, fileName);

				using (var fs = new FileStream(fullPath, FileMode.Create))
				{
					await value.FileImage.CopyToAsync(fs);
				}
				var urlForClient = "/Images/" + fileName;
				value.Image = urlForClient;
			}
			await service.UpdateItem(id, value);
        }

        // DELETE api/<SkillController>/5
        [HttpDelete("{id}")]
		[Authorize(Roles = "admin")]
		public async Task Delete(int id)
        {
            await service.DeleteItem(id);
        }
    }
}
