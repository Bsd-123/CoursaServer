using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class ProgressController : ControllerBase
    {
        private readonly IServiceDouble<ProgressDto> service;
        public ProgressController(IServiceDouble<ProgressDto> service)
        {
            this.service = service;
        }
        // GET: api/<SkillController>
        [HttpGet]
		[Authorize(Roles = "owner,admin")]
		public async Task<List<ProgressDto>> Get()
        {
			var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

			if (currentUserRole == "owner")
			{
                return ((await service.GetAll()).Where(p=> p.Lesson.Course.Owner.UserId == currentUserId)).ToList();
			}
			return await service.GetAll();
        }

        // GET api/<SkillController>/5
        [HttpGet("{id}")]
		[Authorize]
		public async Task<ProgressDto> Get(int id1, int id2)
        {
			var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var progress = await service.GetById(id1, id2);
            if (currentUserRole != "admin" && (progress.UserId != currentUserId || progress.Lesson.Course.Owner.UserId != currentUserId))
            {
                Response.StatusCode = 403; // Forbidden
                return null;
            }
            return progress;
        }

        // POST api/<SkillController>
        [HttpPost]
		[Authorize]
		public async Task<ProgressDto> Post([FromBody] ProgressDto value)
        {
			var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (currentUserRole != "admin" && value.UserId != currentUserId)
            {   
                Response.StatusCode = 403; // Forbidden
                return null;
                
            }
            return await service.AddItem(value);
		}

        // PUT api/<SkillController>/5
        [HttpPut("{id}")]
		[Authorize]
		public async Task Put(int id1, int id2, [FromForm] ProgressDto value)
        {
			var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

			if (currentUserRole != "admin" && id1 != currentUserId)
			{
				Response.StatusCode = 403; // Forbidden
				return;

			}
			await service.UpdateItem(id1, id2, value);
        }

        // DELETE api/<SkillController>/5
        [HttpDelete("{id}")]
		[Authorize]
		public async Task Delete(int id1, int id2)
        {
			var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
			if (currentUserRole != "admin" && id1 != currentUserId)
			{
				Response.StatusCode = 403; // Forbidden
				return;

			}
			await service.DeleteItem(id1, id2);
        }
    }
}
