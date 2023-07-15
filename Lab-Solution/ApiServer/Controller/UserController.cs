using AutoMapper;
using BussinessObject.Models;
using DataAccess.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiServer.Controller
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public UserController(UserManager<ApplicationUser> userManager,
            ILogger<UserController> logger,
            IMapper mapper,
            IAuthManager authManager)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return Ok(user);
        }

        [HttpPost]
        //[Authorize(Roles = "ADMINISTRATOR")]
        [Route("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ApplicationUser user)
        {
            await _userManager.UpdateAsync(user);
            return Ok(user);
        }

        [HttpDelete]
        //[Authorize(Roles = "ADMINISTRATOR")]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
            return Ok(user);
        }
    }
}
