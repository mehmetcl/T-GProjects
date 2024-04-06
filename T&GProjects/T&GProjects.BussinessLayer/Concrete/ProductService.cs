using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T_GProjects.BussinessLayer.Abstract;
using T_GProjects.DataAccessLayer.Abstract;
using T_GProjects.DataAccessLayer.UnitOfWork;
using T_GProjects.EntityLayer.Concrete;

namespace T_GProjects.BussinessLayer.Concrete
{
    public class ProductService:GenericService<Product>,IProductService
    {
        private readonly IProductDal _productDal;
        private readonly IUnitOfWork _unitofWork;

        public ProductService(IProductDal productDal, IUnitOfWork unitofWork):base(productDal,unitofWork)
        {
            _productDal = productDal;
            _unitofWork = unitofWork;
        }

        public async Task<List<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return await _productDal.GetByCategoryIdAsync(categoryId);
        }
    }
}
