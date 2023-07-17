using AutoMapper;
using BussinessObject.Configuration;
using BussinessObject.Models;
using DataAccess.Dtos;
using DataAccess.IRepository;
using DataAccess.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace ApiServer.Controller
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthManager _authManager;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;

        public OrderController(IUnitOfWork unitOfWork, IAuthManager authManager,
            ILogger<OrderController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _authManager = authManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _unitOfWork.Orders.GetAll(include: act => act.Include(o => o.OrderDetails).Include(o => o.Member));
            return Ok(orders);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _unitOfWork.Orders.Get(expression: o => o.OrderId == id, include: act => act.Include(o => o.OrderDetails).Include(o => o.Member));
            return Ok(order);
        }

        [HttpGet]
        //[Authorize]
        [Route("getByUserId")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var orders = await _unitOfWork.Orders.GetAll(expression: o => o.MemberId == userId, include: act => act.Include(o => o.OrderDetails).Include(o => o.Member));
            return Ok(orders);
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> Insert([FromBody] OrderDTO orderDTO)
        {
            var order = _mapper.Map<Order>(orderDTO);
            var userId = _authManager.GetUserId();
            order.MemberId = userId;
            order.OrderDate = DateTime.Now;
            await _unitOfWork.Orders.Insert(order);
            await _unitOfWork.Save();
            //await _unitOfWork.OrderDetails.InsertRange(order.OrderDetails);
            //await _unitOfWork.Save();
            return Ok(order);
        }

        [HttpPut]
        //[Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderUpdateDto payload)
        {
            var order = await _unitOfWork.Orders.Get(p => p.OrderId == id);
            var updateValue = _mapper.Map<Order>(payload);
            order.SetValue(updateValue);
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.Save();
            return Ok(order);
        }

        [HttpDelete]
        //[Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _unitOfWork.Orders.Get(expression: o => o.OrderId == id, include: act => act.Include(o => o.OrderDetails));
            await _unitOfWork.Orders.Delete(id);
            _unitOfWork.OrderDetails.DeleteRange(order.OrderDetails);
            await _unitOfWork.Save();
            return Ok();
        }
    }
}
