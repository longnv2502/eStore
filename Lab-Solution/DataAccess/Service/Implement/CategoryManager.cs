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
    public class CategoryManager : ICategoryManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Category> _categories;

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _categories = unitOfWork.Categories;
        }

        public Task<IList<Category>> GetAll(Expression<Func<Category, bool>> expression = null!,
            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null!,
            Func<IQueryable<Category>, IIncludableQueryable<Category, object>> include = null!) => _categories.GetAll(expression, orderBy, include);

        public Task<Category?> GetById(int id, Func<IQueryable<Category>, IIncludableQueryable<Category, object>> include = null!) => _categories.Get(expression: o => o.CategoryId == id, include);

        public async Task<Category> Insert(Category category)
        {
            var insert = await _categories.Insert(category);
            await _unitOfWork.Save();
            return insert;
        }

        public async Task<Category> Update(Category category)
        {
            var update = _categories.Update(category);
            await _unitOfWork.Save();
            return update;
        }

        public async Task<Category?> Patch(int id, JsonPatchDocument document)
        {
            var patch = await _categories.Patch(document, id);
            await _unitOfWork.Save();
            return patch;
        }

        public async Task<Category?> Delete(int id)
        {
            var delete = await _categories.Delete(id);
            await _unitOfWork.Save();
            return delete;
        }
    }
}
