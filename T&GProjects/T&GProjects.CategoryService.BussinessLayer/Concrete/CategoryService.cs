using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_GProjects.CategoryService.BussinessLayer.Abstract;
using T_GProjects.CategoryService.DataAccessLayer.Abstract;
using T_GProjects.CategoryService.DataAccessLayer.UnitOfWork;
using T_GProjects.EntityLayer.Concrete;

namespace T_GProjects.CategoryService.BussinessLayer.Concrete
{
    public class CategoryService: GenericService<Category>, ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly IUnitOfWork _unitofWork;

        public CategoryService(ICategoryDal categoryDal, IUnitOfWork unitofWork) : base(categoryDal, unitofWork)
        {
            _categoryDal = categoryDal;
            _unitofWork = unitofWork;
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await _categoryDal.GetByNameAsync(name);
        }
    }
}
