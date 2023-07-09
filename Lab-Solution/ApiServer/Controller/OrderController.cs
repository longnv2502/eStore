using AutoMapper;
using BussinessObject.Configuration;
using BussinessObject.Models;
using DataAccess.Dtos;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiServer.Controller
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;

        public OrderController(IUnitOfWork unitOfWork, ILogger<OrderController> logger,
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
            var orders = await _unitOfWork.Orders.GetAll(include: act => act.Include(o => o.OrderDetails).Include(o => o.User));
            return Ok(orders);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _unitOfWork.Orders.Get(expression: o => o.OrderId == id, include: act => act.Include(o => o.OrderDetails).Include(o => o.User));
            return Ok(order);
        }

        [HttpGet]
        [Route("{userId:int}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var orders = await _unitOfWork.Orders.GetAll(expression: o => o.MemberId == userId, include: act => act.Include(o => o.OrderDetails).Include(o => o.User));
            return Ok(orders);
        }

        [HttpPost, Authorize]
        [Route("")]
        public async Task<IActionResult> Insert([FromBody] OrderDTO orderDTO)
        {
            var order = _mapper.Map<Order>(orderDTO);
            order.OrderDate = DateTime.Now;
            await _unitOfWork.OrderDetails.InsertRange(order.OrderDetails);
            await _unitOfWork.Orders.Insert(order);
            await _unitOfWork.Save();
            return Ok(order);
        }

        [HttpPut, Authorize]
        [Route("")]
        public async Task<IActionResult> Update([FromBody] Order order)
        {
            _unitOfWork.OrderDetails.UpdateRange(order.OrderDetails);
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.Save();
            return Ok(order);
        }

        [HttpDelete, Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = _unitOfWork.Orders.Delete(id);
            await _unitOfWork.Save();
            return Ok(order);
        }
    }
}
