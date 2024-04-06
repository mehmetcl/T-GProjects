using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using T_GProjects.CategoryService.BussinessLayer.Abstract;
using T_GProjects.EntityLayer.Concrete;
using T_GProjects.EntityLayer.Dtos;

namespace T_GProjects.CategoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //Endpoint that brings all categories
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {         
            return Ok(await _categoryService.GetAllAsync());

        }
        //endpoint that fetches a specific category by ID
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            if (response == null)
            {

                return BadRequest("Category is not Found!");

            }
            return Ok(response);
        }
        // endpoint that brings a specific category by name
        [HttpGet("[action]/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var response = await _categoryService.GetByNameAsync(name);
            if (response == null)
            {

                return NotFound("CategoryName is not Found!");

            }
            return Ok(response);
        }
        // endpoint that adds a new category
        [HttpPost("[action]")]
        public async Task<IActionResult> Save(CategoryDto category)
        {
            Category dummyCat = _categoryService.GetByNameAsync(category.Name).Result;
            if (dummyCat != null) {
                return BadRequest("This category exists");
            }
            Category newCategory= new() { Name = category.Name};
            return Ok(await _categoryService.AddAsync(newCategory));
        }
        // Endpoint that updates a category
        [HttpPut("[action]")]
        public IActionResult Update(Category category)
        {
            Category dummyCat = _categoryService.GetByIdAsync(category.Id).Result;
            if (dummyCat == null)
            {
                return BadRequest("This category is not found");
            }
            return Ok( _categoryService.UpdateAsync(category));
        }
        // endpoint that deletes a category
        [HttpDelete("[action]")]
        public IActionResult Delete(Category category)
        {
            Category dummyCat = _categoryService.GetByIdAsync(category.Id).Result;
            if (dummyCat == null)
            {
                return BadRequest("This category is not found");
            }
            return Ok(_categoryService.RemoveAsync(category));
        }
    }
}
