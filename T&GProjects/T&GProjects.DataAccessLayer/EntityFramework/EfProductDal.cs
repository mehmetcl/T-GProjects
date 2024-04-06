using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using T_GProjects.DataAccessLayer.Abstract;
using T_GProjects.DataAccessLayer.Concrete;
using T_GProjects.EntityLayer.Concrete;

namespace T_GProjects.DataAccessLayer.EntityFramework
{
    public class EfProductDal : GenericDal<Product>, IProductDal
    {
        public EfProductDal(ProductDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return await _dbContext.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
        }
    }
}
