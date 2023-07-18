using AutoMapper;
using BussinessObject.Configuration;
using BussinessObject.Models;
using DataAccess.Dtos;
using DataAccess.IRepository;
using DataAccess.Service.Implement;
using DataAccess.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static NuGet.Packaging.PackagingConstants;

namespace ApiServer.Controller
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;
        private readonly IAuthManager _authManager;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;

        public OrderController(IOrderManager orderManager, IAuthManager authManager,
            ILogger<OrderController> logger,
            IMapper mapper)
        {
            _orderManager = orderManager;
            _authManager = authManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        [EnableQuery]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderManager.GetAll(include: act => act.Include(o => o.OrderDetails).Include(o => o.Member));
            return Ok(orders);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderManager.GetById(id, include: act => act.Include(o => o.OrderDetails).Include(o => o.Member));
            return Ok(order);
        }

        [HttpGet]
        [Authorize]
        [Route("history")]
        [EnableQuery]
        public async Task<IActionResult> HistoryOrder()
        {
            var userId = _authManager.GetUserId();
            var orders = await _orderManager.GetAll(expression: o => o.MemberId == userId, include: act => act.Include(o => o.OrderDetails));
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
            DateTime now = DateTime.Now;
            order.OrderDate = now.Trim(TimeSpan.TicksPerSecond);
            var insert = await _orderManager.Insert(order);
            return Ok(insert);
        }

        [HttpPut]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderUpdateDto payload)
        {
            var order = await _orderManager.GetById(id);
            if (order == null) return NotFound();
            var updateValue = _mapper.Map<Order>(payload);
            order.SetValue(updateValue);
            var update = await _orderManager.Update(order);
            return Ok(update);
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _orderManager.Delete(id);
            return Ok(delete);
        }
    }
}
