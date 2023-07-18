using AutoMapper;
using BussinessObject.Models;
using DataAccess.Dtos;
using DataAccess.IRepository;
using DataAccess.Service.Implement;
using DataAccess.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
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
        private readonly IProductManager _productManager;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;

        public ProductController(IProductManager productManager, ILogger<ProductController> logger,
            IMapper mapper)
        {
            _productManager = productManager;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("")]
        [EnableQuery]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productManager.GetAll(include: act => act.Include(p0 => p0.Category));
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productManager.GetById(id, act => act.Include(p0 => p0.Category));
            return Ok(product);
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Route("")]
        public async Task<IActionResult> Insert([FromBody] Product product)
        {
            var insert = await _productManager.Insert(product);
            return Ok(insert);
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto payload)
        {
            var product = await _productManager.GetById(id);
            if (product == null) return NotFound();
            var updateValue = _mapper.Map<Product>(payload);
            product.SetValue(updateValue);
            var update = await _productManager.Update(product);
            return Ok(update);
        }

        [HttpPatch]
        [Authorize(Roles = "Administrator")]
        [Route("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument document)
        {
            var patch = await _productManager.Patch(id, document);
            return Ok(patch);
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _productManager.Delete(id);
            return Ok(delete);
        }
    }
}
