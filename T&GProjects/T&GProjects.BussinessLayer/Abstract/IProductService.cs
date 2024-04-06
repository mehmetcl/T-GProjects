using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_GProjects.EntityLayer.Concrete;

namespace T_GProjects.BussinessLayer.Abstract
{
    public interface IProductService:IGenericService<Product>
    {
        Task<List<Product>> GetByCategoryIdAsync(int categoryId);

    }
}
