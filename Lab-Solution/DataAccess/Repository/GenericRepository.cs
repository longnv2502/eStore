using DataAccess.IRepository;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using BussinessObject.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Security.Principal;

namespace DataAccess.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EStoreContext _context;
        private readonly DbSet<T> _db;

        public GenericRepository(EStoreContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _db;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IPagedList<T>> GetPagedList(Pageable pageable, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _db;


            if (include != null)
            {
                query = include(query);
            }

            return await query.AsNoTracking()
                .ToPagedListAsync(pageable.PageNumber, pageable.PageSize);
        }

        public async Task<T?> Get(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _db;
            if (include != null)
            {
                query = include(query);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<T> Insert(T entity)
        {
            await _db.AddAsync(entity);
            return entity;
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);
        }

        public async Task<T?> Patch(JsonPatchDocument patch, params object[] keyValues)
        {
            var entity = await _db.FindAsync(keyValues);
            if (entity == null) return null;
            patch.ApplyTo(entity);
            Update(entity);
            return entity;
        }

        public async Task<T> Delete(params object[] keyValues)
        {
            var entity = await _db.FindAsync(keyValues);
            if (entity == null) return null;
            _db.Remove(entity);
            return entity;
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        public T Update(T entity)
        {
            _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _db.AttachRange(entities);
            entities.ToList().ForEach(entity => _context.Entry(entity).State = EntityState.Modified);
        }
    }
}
