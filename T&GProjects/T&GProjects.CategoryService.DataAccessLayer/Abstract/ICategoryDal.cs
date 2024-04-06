using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_GProjects.EntityLayer.Concrete;

namespace T_GProjects.CategoryService.DataAccessLayer.Abstract
{
    public interface ICategoryDal : IGenericDal<Category>
    {
        Task<Category> GetByNameAsync(string name);

    }
}
