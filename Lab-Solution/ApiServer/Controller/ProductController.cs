using AutoMapper;
using BussinessObject.Models;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            var products = await _unitOfWork.Products.GetAll(include: p => p.Include(p0 => p0.Category));
            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _unitOfWork.Products.Get(p => p.ProductId == id, p => p.Include(p0 => p0.Category));
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> SearchProductByName(string? Keyword)
        {
            Expression<Func<Product, bool>> expression = (p) => true;
            if (Keyword != null)
            {
                expression = expression.And(p => p.ProductName.Contains(Keyword));
            }
            var products = await _unitOfWork.Products.GetAll(expression: expression, include: p => p.Include(p0 => p0.Category));
            return Ok(products);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _unitOfWork.Products.Insert(product);
            _unitOfWork.Save();
            return Ok("Insert Successfull!!!");
        }

        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            _unitOfWork.Products.Update(product);
            _unitOfWork.Save();
            return Ok("Update Successfull!!!");
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            _unitOfWork.Products.Delete(id);
            _unitOfWork.Save();
            return Ok("Delete Successfull!!!");
        }
    }
}
