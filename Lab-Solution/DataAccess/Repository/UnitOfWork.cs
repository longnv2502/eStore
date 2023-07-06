using BussinessObject.Models;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EStoreContext _context;
        private IGenericRepository<Cart> _cart;
        private IGenericRepository<CartDetail> _cartDetails;
        private IGenericRepository<Category> _categories;
        private IGenericRepository<Order> _orders;
        private IGenericRepository<OrderDetail> _orderDetails;
        private IGenericRepository<Product> _products;

        public UnitOfWork(EStoreContext context)
        {
            _context = context;
        }
        public IGenericRepository<Cart> Carts => _cart ??= new GenericRepository<Cart>(_context);
        public IGenericRepository<CartDetail> CartDetails => _cartDetails ??= new GenericRepository<CartDetail>(_context);
        public IGenericRepository<Category> Categories => _categories ??= new GenericRepository<Category>(_context);
        public IGenericRepository<Order> Orders => _orders ??= new GenericRepository<Order>(_context);
        public IGenericRepository<OrderDetail> OrderDetails => _orderDetails ??= new GenericRepository<OrderDetail>(_context);
        public IGenericRepository<Product> Products => _products ??= new GenericRepository<Product>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
