using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Dto;
using Service.Interfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IService<OwnerDto> service;
        public OwnerController(IService<OwnerDto> service)
        {
            this.service = service;
        }
        // GET: api/<OwnerController>
        [HttpGet]

        public async Task<List<OwnerDto>> Get()
        {
            return await service.GetAll();
        }

        // GET api/<OwnerController>/5
        [HttpGet("{id}")]

		public async Task<OwnerDto> Get(int id)
        {
            return await service.GetById(id);
        }

        // POST api/<OwnerController>
        [HttpPost]
        [Authorize]
        public async Task<OwnerDto> Post([FromForm] OwnerDto value)
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
			return await service.AddItem(value);
        }

        // PUT api/<OwnerController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "owner,admin")]
        async public Task Put(int id, [FromForm] OwnerDto value)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var owner = await service.GetById(id);

            if (owner == null)
            {
                Response.StatusCode = 404;
                return;
            }

            if (currentUserRole == "owner")
            {
                if (owner.UserId != currentUserId)
                {
                    Response.StatusCode = 403;
                    return;
                }
            }
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
            await service.UpdateItem(id, value);

        }

        // DELETE api/<OwnerController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "owner,admin")]
        public async Task Delete(int id)
        {
			var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
			var owner = await service.GetById(id);

			if (owner == null)
			{
				Response.StatusCode = 404;
				return;
			}

			if (currentUserRole == "owner")
			{
				if (owner.UserId != currentUserId)
				{
					Response.StatusCode = 403;
					return;
				}
			}
			await service.DeleteItem(id);
        }
    }
}
