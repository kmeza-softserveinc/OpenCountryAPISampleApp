using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenCountryAPISampleApp.EFModels.UsersModel;
using OpenCountryAPISampleApp.Helpers;
using System.Security.Cryptography;

namespace OpenCountryAPISampleApp.Controllers
{
    [Route("/[controller]")]
    public class LoginController : Controller
    {
        private readonly UsersDbContext _dbContext;
        private readonly ILogger<LoginController> _logger;

        public LoginController(UsersDbContext dbContext, ILogger<LoginController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(string? email, string? password)
        {
            if (_dbContext.Users == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) )
            {
                return BadRequest();
            }

            var user = await _dbContext.Users.FindAsync(email = email);

            if (user == null || !Argon2PasswordHasher.VerifyHashedPassword(user.Password, password))
            {
                return Unauthorized();                
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Email)
            };
            
            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");

            await HttpContext.SignInAsync(
                "CookieAuth",
                new ClaimsPrincipal(claimsIdentity));

            _logger.LogInformation("User: {} successfully logged in", user.Email);

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] randomBytes = new byte[16];  // 16 bytes will give us a 32-character hexadecimal string
                rng.GetBytes(randomBytes);
                return Ok(BitConverter.ToString(randomBytes).Replace("-", "").ToLower());
            }

        }
    }
}
