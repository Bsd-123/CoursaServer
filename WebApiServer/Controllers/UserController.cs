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
    public class UserController : ControllerBase
    {
        private readonly IService<UserDto> service;
        // GET: api/<UserController>
        public UserController(IService<UserDto> service)
        {
            this.service = service;
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<List<UserDto>> Get()
        {
            return await service.GetAll();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<UserDto> Get(int id)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var user = await service.GetById(id);
            if (user != null)
            {
                if (currentUserRole == "owner" || currentUserId == user.Id)
                {
                    return await service.GetById(id);
                }
                    Response.StatusCode = 403;
                    return null;
			}
            Response.StatusCode = 404;
            return null;
        }


        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task Put(int id, [FromBody] UserDto value)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (currentUserId != id)
            {
                Response.StatusCode = 403;
                return;
            }
            await service.UpdateItem(id, value);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task Delete(int id)
        {
            var value = await service.GetById(id);
            value.Role = "Delete";
            await service.UpdateItem(id, value);
        }
    }
}
