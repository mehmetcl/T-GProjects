using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_GProjects.CategoryService.DataAccessLayer.Abstract;
using T_GProjects.CategoryService.DataAccessLayer.Concrete;
using T_GProjects.EntityLayer.Concrete;

namespace T_GProjects.CategoryService.DataAccessLayer.EntityFramework
{
    public class EfCategoryDal: GenericDal<Category>,ICategoryDal
    {
        public EfCategoryDal(CategoryDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x=>x.Name==name);
        }
    }
}
