using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.Entities;
using Service.Dto;
using Service.Interfaces;
using Service.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

        public LoginController(ILogin login, IConfiguration configuration)
        {
            this.login = login;
            this.config = configuration;
        }

        // POST api/<LoginController>
        [HttpPost("login")]
        public IActionResult Post([FromBody] UserLogin user)
        {
            var user1 = login.Authenticate(user);
            if (user1 != null)
            {
                return Ok(GenerateToken(user1));
            }
            return BadRequest("user not found...");
        }
        [HttpPost("register")] // הכתובת תהיה api/auth/register
        public IActionResult Register([FromBody] User newUser)
        {
            if(newUser == null || string.IsNullOrEmpty(newUser.Email) || string.IsNullOrEmpty(newUser.Password))
            {
                return BadRequest("נתונים לא תקינים להרשמה");
            }
            if (login.GetByEmail(newUser.Email) != null)
            {
                return BadRequest("משתמש עם האימייל הזה כבר קיים");
            }

            // 2. שמירת המשתמש החדש במסד הנתונים (כולל Hashing לסיסמה!)
            var registeredUser = login.AddUser(newUser);

            if (registeredUser == null)
                return BadRequest("הרישום נכשל או שמשתמש כבר קיים");

            // 3. אופציונלי: החזרת טוקן מיד בסיום ההרשמה כדי שהמשתמש יתחבר אוטומטית
            return Ok(GenerateToken(registeredUser));
        }
        //יצירת טוקן
        private string GenerateToken(User user1)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            //אלגוריתם להצפנה
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
            new Claim(ClaimTypes.Name,user1.Name),
            new Claim(ClaimTypes.Email,user1.Email),
            new Claim(ClaimTypes.Role,user1.Role),
            //new Claim("Id",user1.Id.ToString()),
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
