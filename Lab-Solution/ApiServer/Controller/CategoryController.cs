using AutoMapper;
using BussinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace ApiServer.Controller
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        private readonly ILogger<CategoryController> _logger;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryManager categoryManager, ILogger<CategoryController> logger,
            IMapper mapper)
        {
            _categoryManager = categoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        [EnableQuery]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryManager.GetAll(include: act => act.Include(c => c.Products));
            return Ok(categories);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryManager.GetById(id, include: act => act.Include(c => c.Products));
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Route("")]
        public async Task<IActionResult> Insert([FromBody] Category category)
        {
            var insert = await _categoryManager.Insert(category);
            return Ok(insert);
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category category)
        {
            var update = await _categoryManager.Update(category);
            return Ok(update);
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _categoryManager.Delete(id);
            return Ok(delete);
        }
    }
}
