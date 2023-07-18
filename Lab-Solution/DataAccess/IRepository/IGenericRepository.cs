using DataAccess.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace DataAccess.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAll(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
         );
        Task<IPagedList<T>> GetPagedList(
            Pageable requestParams,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
            );

        Task<T?> Get(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<T> Insert(T entity);
        Task InsertRange(IEnumerable<T> entities);
        Task<T?> Patch(JsonPatchDocument patch, params object[] keyValues);
        Task<T?> Delete(params object[] keyValues);
        void DeleteRange(IEnumerable<T> entities);
        T Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
    }
}
