using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DCommerce.Data.Domain;
using DCommerce.Data.Identity_models;
using DCommerce.Dto.Requests.Login;
using DCommerce.Dto.Requests.Register;
using DCommerce.Dto.Responses;
using DCommerce.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DCommerce.WebApi.Controllers
{
    [Route("/api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthenticationController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            try
            {
                var user = await userManager.FindByNameAsync(model.Username);
                if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(3),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );

                    UserProfileDto currentUser = _mapper.Map<ApplicationUser, UserProfileDto>(user);

                    LoginResponse response = new LoginResponse
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = token.ValidTo,
                        UserProfile = currentUser
                    };

                    return Ok(response);

                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            try
            {
                var userExists = await userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User already exists!" });

                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    ColorTheme = model.ColorTheme
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });

                return Ok(new AuthResponse { Status = "Success", Message = "User created successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            try
            {
                var userExists = await userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User already exists!" });

                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    ColorTheme = model.ColorTheme
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                if (await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await userManager.AddToRoleAsync(user, UserRoles.Admin);
                }

                return Ok(new AuthResponse { Status = "Success", Message = "User created successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("userprofile/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserProfile(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Provided User Id is not a valid string or Guid");
            try
            {
                //var userExists = await userManager.FindByNameAsync(model.Username);
                var userExists = await userManager.FindByIdAsync(id);
                if (userExists == null)
                    return StatusCode(StatusCodes.Status404NotFound, new AuthResponse { Status = "Error", Message = "User Not Found!" });
                UserProfileDto user = _mapper.Map<ApplicationUser, UserProfileDto>(userExists);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
