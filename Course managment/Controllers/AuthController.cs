using CourseManagment.CORE.DTOs.Request;
using CourseManagment.CORE.DTOs.Response;
using CourseManagment.CORE.Identity;
using CourseManagment.CORE.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CourseManagment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, UserManager<ApplicationUser> userManager,
                             SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(CreateUserRequest request)
        {
            //var user =request.Adapt<ApplicationUser>();

            var createdUser = await _userService.CreateUserAsync(request, request.Password);
            var token = await GenerateJwtToken(createdUser);

            return Ok(new AuthResponse
            {
                Token = token,
                User = new UserResponse
                {
                    Id = createdUser.Id,
                    Email = createdUser.Email,
                    FirstName = createdUser.FirstName,
                    LastName = createdUser.LastName,
                    FullName = createdUser.FullName,
                    CompanyId = createdUser.CompanyId,
                    JobTitle = createdUser.JobTitle,
                    Points = createdUser.Points
                }
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
        {
            var user = await _userService.GetUserByEmailAsync(request.Email);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Invalid credentials");

            if (!user.IsActive)
                return Unauthorized("Account is deactivated");

            var token = await GenerateJwtToken(user);

            return Ok(new AuthResponse
            {
                Token = token,
                User = new UserResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FullName = user.FullName,
                    CompanyId = user.CompanyId,
                    JobTitle = user.JobTitle,
                    Points = user.Points
                }
            });
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim("CompanyId", user.CompanyId.ToString()),
                new Claim("FullName", user.FullName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryInMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    
}