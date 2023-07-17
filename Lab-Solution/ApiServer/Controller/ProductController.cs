using AutoMapper;
using BussinessObject.Models;
using DataAccess.Dtos;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Principal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiServer.Controller
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, ILogger<ProductController> logger,
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
            var products = await _unitOfWork.Products.GetAll(include: act => act.Include(p0 => p0.Category));
            return Ok(products);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _unitOfWork.Products.Get(p => p.ProductId == id, act => act.Include(p0 => p0.Category));
            return Ok(product);
        }

        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> FilterByName(string? Keyword)
        {
            Expression<Func<Product, bool>> expression = (p) => true;
            if (Keyword != null)
            {
                expression = expression.And(p => p.ProductName.Contains(Keyword));
            }
            var products = await _unitOfWork.Products.GetAll(expression: expression, include: act => act.Include(p0 => p0.Category));
            return Ok(products);
        }

        [HttpPost]
        //[Authorize(Roles = "ADMINISTRATOR")]
        [Route("")]
        public async Task<IActionResult> Insert([FromBody] Product product)
        {
            await _unitOfWork.Products.Insert(product);
            await _unitOfWork.Save();
            return Ok(product);
        }

        [HttpPut]
        //[Authorize(Roles = "ADMINISTRATOR")]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto payload)
        {
            var product = await _unitOfWork.Products.Get(p => p.ProductId == id);
            var updateValue = _mapper.Map<Product>(payload);
            product.SetValue(updateValue);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.Save();
            return Ok(product);
        }

        [HttpPatch]
        //[Authorize(Roles = "ADMINISTRATOR")]
        [Route("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument patch)
        {
            var product = await _unitOfWork.Products.Patch(patch, id);
            return Ok(product);
        }

        [HttpDelete]
        //[Authorize(Roles = "ADMINISTRATOR")]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.Products.Delete(id);
            await _unitOfWork.Save();
            return Ok(product);
        }
    }
}
