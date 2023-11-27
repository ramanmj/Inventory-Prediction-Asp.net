using InventoryManagement_api.Data;
using InventoryManagement_api.Models;
using InventoryManagement_api.Models.Design;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventoryManagement_api.Controllers
{

    public class AuthController : Controller

    {

        private readonly InventoryDbContext inventoryDbContext;
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration, InventoryDbContext inventoryDbContext)
        {
            _configuration= configuration;
            this.inventoryDbContext = inventoryDbContext;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            var user = await inventoryDbContext.Users.FirstOrDefaultAsync(x =>x.Email  == Email );
            if(Email == null || Password == null)
            {
                return BadRequest("fill the fields");
            }

            if(user == null)
            {
                return BadRequest("no such user");
            }
            if (!BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash)){
                return BadRequest("wrong password");
            }
            var jwtToken = CreateToken(user);

            return Ok(jwtToken);

        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto request)
        {
            var user = new User()
            {
                Username = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Email = request.Email,
                gender = request.gender,
                Phone = request.Phone
            };
            var a = await inventoryDbContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if(a != null) {
                return BadRequest("email already taken ");    
            }
            await inventoryDbContext.Users.AddAsync(user);
            await inventoryDbContext.SaveChangesAsync();
            return Ok(user);

        }


        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username) 
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:token").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                   claims: claims,
                   expires: DateTime.Now.AddDays(1),
                   signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
