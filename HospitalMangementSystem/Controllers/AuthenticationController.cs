﻿using HospitalMangement.Domain.Models.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalMangementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        



        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                  {
                      new Claim(ClaimTypes.Name, user.UserName),
                      
                      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                  };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }





        [HttpPost]
        [Route("register-receptionist")]
        public async Task<IActionResult> Register_Receptionist([FromBody] RegisterModel register)
        {
            // check user Exit
            var userExit = await _userManager.FindByNameAsync(register.Username);
            if (userExit != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                  new Response { Status = "Error", Message = "User already exists" });
            }

            // Identity User
            IdentityUser user = new()
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.Username
            };
            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)

                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (await _roleManager.RoleExistsAsync(UserRoles.Receptionist))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Receptionist);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
        // Doctor Register
        [HttpPost]
        [Route("register-doctor")]
        public async Task<IActionResult> Register_doctor([FromBody] RegisterModel register)
        {
            // check user Exit
            var userExit = await _userManager.FindByNameAsync(register.Username);
            if (userExit != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                  new Response { Status = "Error", Message = "User already exists" });
            }
            // Identity User
            IdentityUser user = new()
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.Username
            };
            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)

                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (await _roleManager.RoleExistsAsync(UserRoles.Doctor))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Doctor);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        // JWT Token Function
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

    }
    }

