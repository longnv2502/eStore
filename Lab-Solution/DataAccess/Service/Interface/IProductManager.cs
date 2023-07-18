using BussinessObject.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service.Interface
{
    public interface IProductManager
    {
        Task<IList<Product>> GetAll(Expression<Func<Product, bool>> expression = null!,
            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null!,
            Func<IQueryable<Product>, IIncludableQueryable<Product, object>> include = null!);
        Task<Product?> GetById(int id, Func<IQueryable<Product>, IIncludableQueryable<Product, object>> include = null!);
        Task<Product> Insert(Product product);
        Task<Product> Update(Product product);
        Task<Product?> Patch(int id, JsonPatchDocument document);
        Task<Product?> Delete(int id);
    }
}
