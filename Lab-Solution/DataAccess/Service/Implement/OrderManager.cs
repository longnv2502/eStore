using AutoMapper;
using BussinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Service.Interface;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service.Implement
{
    public class OrderManager : IOrderManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Order> _orders;

        public OrderManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _orders = unitOfWork.Orders;
        }

        public Task<IList<Order>> GetAll(Expression<Func<Order, bool>> expression = null!,
            Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null!,
            Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include = null!) => _orders.GetAll(expression, orderBy, include);

        public Task<Order?> GetById(int id, Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include = null!) => _orders.Get(expression: o => o.OrderId == id, include);

        public async Task<Order> Insert(Order order)
        {
            var insert = await _orders.Insert(order);
            await _unitOfWork.Save();
            return insert;
        }

        public async Task<Order> Update(Order order)
        {
            var update = _orders.Update(order);
            await _unitOfWork.Save();
            return update;
        }

        public async Task<Order?> Patch(int id, JsonPatchDocument document)
        {
            var patch = await _orders.Patch(document, id);
            await _unitOfWork.Save();
            return patch;
        }

        public async Task<Order?> Delete(int id)
        {
            var delete = await _orders.Delete(id);
            await _unitOfWork.Save();
            return delete;
        }
    }
}
