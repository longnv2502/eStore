using AutoMapper;
using DataAccess.IRepository;
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
            var orders = await _unitOfWork.Orders.GetAll();
            return Ok(orders);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _unitOfWork.Orders.Get(o => o.OrderId == id);
            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var orders = await _unitOfWork.Orders.GetAll(expression: o => o.MemberId == userId);
            return Ok(orders);
        }
    }
}
