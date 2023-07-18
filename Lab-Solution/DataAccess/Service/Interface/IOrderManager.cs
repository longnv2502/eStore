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
    public interface IOrderManager
    {
        Task<IList<Order>> GetAll(Expression<Func<Order, bool>> expression = null!,
            Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null!,
            Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include = null!);
        Task<Order?> GetById(int id, Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include = null!);
        Task<Order> Insert(Order category);
        Task<Order> Update(Order category);
        Task<Order?> Patch(int id, JsonPatchDocument document);
        Task<Order?> Delete(int id);
    }
}
