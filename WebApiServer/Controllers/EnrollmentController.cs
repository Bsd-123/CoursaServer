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
    public class EnrollmentController : ControllerBase
    {
        private readonly IServiceDouble<EnrollmentDto> service;
        public EnrollmentController(IServiceDouble<EnrollmentDto> service)
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

        // GET api/<EnrollmentController>/5
        [HttpGet("{id}")]
		[Authorize(Roles = "owner,admin")]
		public async Task<EnrollmentDto> Get(int id1, int id2)
        {
			var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

			// 2. שליפת הנתון מהשירות
			var enrollment = await service.GetById(id1, id2);

            if (enrollment == null) { 
                Response.StatusCode = 404;
                return null;
            }

			if (currentUserRole == "owner")
			{
				if (enrollment.Course.OwnerId != currentUserId)
				{
					Response.StatusCode = 403;
					return null;
				}
			}
			return enrollment;
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
        public async Task Put(int id1, int id2, [FromForm] EnrollmentDto value)
        {
            await service.UpdateItem(id1, id2, value);
        }

		// DELETE api/<EnrollmentController>/5
		[Authorize(Roles = "admin")]
		[HttpDelete("{id}")]
        public async Task Delete(int id1, int id2)
        {
            await service.DeleteItem(id1, id2);
        }
    }
}
