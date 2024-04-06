using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_GProjects.EntityLayer.Concrete;

namespace T_GProjects.DataAccessLayer.Abstract
{
    public interface IProductDal : IGenericDal<Product>
    {
        Task<List<Product>> GetByCategoryIdAsync(int categoryId);

    }
}
