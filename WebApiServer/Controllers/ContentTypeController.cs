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
		[Authorize]
		public async Task<List<ContentTypeDto>> Get()
        {
            return await service.GetAll();
        }

        // GET api/<ContentTypeController>/5
        [HttpGet("{id}")]
        public async Task<ContentTypeDto> Get(int id)
        {
            return await service.GetById(id);
        }

        // POST api/<ContentTypeController>
        [HttpPost]
        [Authorize(Roles = "owner")]
        public async Task<ContentTypeDto> Post([FromForm] ContentTypeDto value)
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
				value.DisplayIcon = urlForClient;
			}
			return await  service.AddItem(value);
        }

        // PUT api/<ContentTypeController>/5
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "owner")]
        public async Task Put(int id, [FromForm] ContentTypeDto value)
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
                    await  value.FileImage.CopyToAsync(fs); 
                }
                var urlForClient = "/Images/" + fileName;
                value.DisplayIcon = urlForClient;
            }
            await service.UpdateItem(id, value);
        }

        // DELETE api/<ContentTypeController>/5
        [HttpDelete("{id}")]
		[Authorize(Roles = "admin")]
		public async Task Delete(int id)
        {
            await service.DeleteItem(id);
        }
    }
}
