using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.Entities;
using Service.Dto;
using Service.Interfaces;
using Service.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin login;
        private readonly IConfiguration config;
        private readonly UserToAddingService userToAddingService;

        public LoginController(ILogin login, IConfiguration configuration, UserToAddingService userToAddingService)
        {
            this.login = login;
            this.config = configuration;
            this.userToAddingService = userToAddingService;
        }

        // POST api/<LoginController>
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] UserForLogin user)
        {
            var user1 =await  login.Authenticate(user);
            if (user1 != null)
            {
                return Ok(new { user = user1, token = GenerateToken(user1) });
            }
            return BadRequest("user not found...");
        }
        [HttpPost("register")] // הכתובת תהיה api/auth/register
        public async Task<IActionResult> Register([FromBody] UserToAdding newUser)
        {
            if(newUser == null || string.IsNullOrEmpty(newUser.Email) || string.IsNullOrEmpty(newUser.Password))
            {
                return BadRequest("נתונים לא תקינים להרשמה");
            }
            //to check again!!!
            //if (login.GetByUserEmail(newUser.Id) != null)
            //{
            //    return BadRequest("משתמש עם האימייל הזה כבר קיים");
            //}

            // 2. שמירת המשתמש החדש במסד הנתונים (כולל Hashing לסיסמה!)
            var registeredUser =await userToAddingService.AddItem(newUser);

            if (registeredUser == null)
                return BadRequest("הרישום נכשל או שמשתמש כבר קיים");

            // 3. אופציונלי: החזרת טוקן מיד בסיום ההרשמה כדי שהמשתמש יתחבר אוטומטית
            return Ok(new { user = registeredUser, token = GenerateToken(registeredUser) });
        }
        [HttpGet("GetUserByToken")]
        [Authorize] // זה מבטיח שה-Token נבדק אוטומטית
        public async Task<UserDto> GetUserByToken()
        {
            // המידע על המשתמש (כמו ID או Name) נמצא בתוך ה-Claims של ה-Identity
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId != null)
            {
                // כאן את יכולה לשלוף שוב את פרטי המשתמש מהדיאטבייס במידת הצורך
                return await login.GetByUserId(userId);
            }

            return null;
        }
        //יצירת טוקן
        private string GenerateToken(UserDto user1)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            //אלגוריתם להצפנה
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier,user1.Id.ToString()),
            new Claim(ClaimTypes.Email,user1.Email),
            new Claim(ClaimTypes.Role,user1.Role),
            //new Claim(ClaimTypes.NameIdentifier,user1.Id.ToString()),
            //new Claim(ClaimTypes.GivenName,user1.Name)
            };
            var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
    }
}
