using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T_GProjects.EntityLayer.Dtos
{
    public class ProductSaveWithCategoryNameDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string CategoryName { get; set; }
    }
}
