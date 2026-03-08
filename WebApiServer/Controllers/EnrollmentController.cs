using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Interfaces;
using Service.Dto;
using Service.Interfaces;
using Service.Services;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly EnrollmentService service;
        public EnrollmentController(EnrollmentService service)
        {
            this.service = service;
        }
        // GET: api/<EnrollmentController>
        [HttpGet]
		[Authorize(Roles = "admin")]
		public async Task<List<EnrollmentDto>> Get()
        {
            return await service.GetAll();
        }
        [HttpGet("myCourses")]
        [Authorize]
        public async Task<List<EnrollmentDto>> GetByUserId()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return await service.GetByUserId(userId);
        }
        [HttpGet("myEnrollments/{id}")]
        [Authorize]
        public async Task<List<EnrollmentDto>> GetMineByCourseId(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return await service.GetMineByCourseId(id, userId);
        }

        // GET api/<EnrollmentController>/5 מה פשרה של הפעולה?
        [HttpGet("{id}")]
		[Authorize(Roles = "owner,admin")]
		public async Task<EnrollmentDto> Get(int id)
        {
			var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            return await service.GetById(id, userId, userRole);
        }

        // POST api/<EnrollmentController>
        [HttpPost]
		[Authorize]
		public async Task<EnrollmentDto> Post([FromBody] EnrollmentDto  value)
        {
            return await service.AddItem(value);
        }

		// PUT api/<EnrollmentController>/5 
		
		[HttpPut("{id}")]
        [Authorize]
        public async Task Put(int id, [FromForm] EnrollmentDto value)
        {
            await service.UpdateItem(id, value);
        }

		// DELETE api/<EnrollmentController>/5
		[Authorize(Roles = "admin")]
		[HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.DeleteItem(id);
        }
    }
}
