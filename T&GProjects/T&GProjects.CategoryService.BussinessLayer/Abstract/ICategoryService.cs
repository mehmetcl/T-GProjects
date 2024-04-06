using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_GProjects.EntityLayer.Concrete;

namespace T_GProjects.CategoryService.BussinessLayer.Abstract
{
    public interface ICategoryService : IGenericService<Category>
    {
        Task<Category> GetByNameAsync(string name);

    }
}
