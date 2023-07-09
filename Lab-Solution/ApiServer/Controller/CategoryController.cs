using AutoMapper;
using BussinessObject.Models;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiServer.Controller
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryController> _logger;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, ILogger<CategoryController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAll(include: act => act.Include(c => c.Products));
            return Ok(categories);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _unitOfWork.Categories.Get(expression: c => c.CategoryId == id, include: act => act.Include(c => c.Products));
            return Ok(category);
        }

        [HttpPost, Authorize(Roles = "ADMINISTRATOR")]
        [Route("")]
        public async Task<IActionResult> Insert([FromBody] Category category)
        {
            await _unitOfWork.Categories.Insert(category);
            await _unitOfWork.Save();
            return Ok(category);
        }

        [HttpPut, Authorize(Roles = "ADMINISTRATOR")]
        [Route("")]
        public async Task<IActionResult> Update([FromBody] Category category)
        {
            _unitOfWork.Categories.Update(category);
            await _unitOfWork.Save();
            return Ok(category);
        }

        [HttpDelete, Authorize(Roles = "ADMINISTRATOR")]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = _unitOfWork.Categories.Delete(id);
            await _unitOfWork.Save();
            return Ok(category);
        }
    }
}
