﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Ticket.IdentityServer.DTOs;
using Ticket.IdentityServer.Models;
using Ticket.Shared.DTOs;
using static IdentityServer4.IdentityServerConstants;

namespace Ticket.IdentityServer.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupDto signupDto)
        {
            var user = new ApplicationUser
            {
                Email = signupDto.Email,
                UserName = signupDto.UserName,
                City = signupDto.City
            };

            var result = await _userManager.CreateAsync(user, signupDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
            }

            return NoContent();
        }

        [HttpGet]
		[Authorize(LocalApi.PolicyName)]
		public async Task<IActionResult> GetUser()
        {
            var userClaim = User.Claims.FirstOrDefault(X=> X.Type == JwtRegisteredClaimNames.Sub);

            if (userClaim == null) return BadRequest();

            var user = await _userManager.FindByIdAsync(userClaim.Value);

            if (user == null) return BadRequest();

            return Ok(new { Id = user.Id, UserName = user.UserName, Email = user.Email, City=user.City }) ;

        }
    }
}
