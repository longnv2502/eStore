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
    public interface ICategoryManager
    {
        Task<IList<Category>> GetAll(Expression<Func<Category, bool>> expression = null!,
            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null!,
            Func<IQueryable<Category>, IIncludableQueryable<Category, object>> include = null!);
        Task<Category?> GetById(int id, Func<IQueryable<Category>, IIncludableQueryable<Category, object>> include = null!);
        Task<Category> Insert(Category category);
        Task<Category> Update(Category category);
        Task<Category?> Patch(int id, JsonPatchDocument document);
        Task<Category?> Delete(int id);
    }
}
