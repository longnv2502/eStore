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
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _products;

        public ProductManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _products = unitOfWork.Products;
        }

        public Task<IList<Product>> GetAll(Expression<Func<Product, bool>> expression = null!,
            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null!,
            Func<IQueryable<Product>, IIncludableQueryable<Product, object>> include = null!) => _products.GetAll(expression, orderBy, include);

        public Task<Product?> GetById(int id, Func<IQueryable<Product>, IIncludableQueryable<Product, object>> include = null!) => _products.Get(expression: o => o.ProductId == id, include);

        public async Task<Product> Insert(Product product)
        {
            var insert = await _products.Insert(product);
            await _unitOfWork.Save();
            return insert;
        }

        public async Task<Product> Update(Product product)
        {
            var update = _products.Update(product);
            await _unitOfWork.Save();
            return update;
        }

        public async Task<Product?> Patch(int id, JsonPatchDocument document)
        {
            var patch = await _products.Patch(document, id);
            await _unitOfWork.Save();
            return patch;
        }

        public async Task<Product?> Delete(int id)
        {
            var delete = await _products.Delete(id);
            await _unitOfWork.Save();
            return delete;
        }
    }
}
