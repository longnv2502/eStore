using AutoMapper;
using BussinessObject.Models;
using DataAccess.Dtos;
using DataAccess.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
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
        [EnableQuery]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return Ok(user);
        }

        [HttpGet]
        [Route("profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userId = _authManager.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return Ok(user);
        }

        [HttpPut]
        [Route("profile")]
        [Authorize]
        public async Task<IActionResult> EditProfile([FromBody] ProfileUpdateDto payload)
        {
            var userId = _authManager.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var updateValue = _mapper.Map<ApplicationUser>(payload);
            user.SetValue(updateValue);
            await _userManager.UpdateAsync(user);
            return Ok(user);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Route("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ApplicationUser user)
        {
            await _userManager.UpdateAsync(user);
            return Ok(user);
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
            return Ok(user);
        }
    }
}
