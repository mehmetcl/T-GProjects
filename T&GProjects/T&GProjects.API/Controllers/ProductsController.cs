using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using System.Net;
using T_GProjects.BussinessLayer.Abstract;
using T_GProjects.EntityLayer.Concrete;
using T_GProjects.EntityLayer.Dtos;

namespace T_GProjects.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public HttpClient _httpClient;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
            _httpClient = new HttpClient();
        }


        //GetAll method: Gets all products.
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAllAsync());
        }

        //GetById method: Returns the product with a specific ID.
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _productService.GetByIdAsync(id));
        }


        //Save method:endpoint that adds a new product
        [HttpPost("[action]")]
        public async Task<IActionResult> Save(Product product)
        {

            try
            {
                //Categories API Consume
                string apiUrl = "https://localhost:7181/api/Categories/GetById/" + product.CategoryId;

                var response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return BadRequest("Fail!!! Http Code: " + response.StatusCode + " Error message: " + response.Content.ReadAsStringAsync().Result);
                }

                Category category = await response.Content.ReadFromJsonAsync<Category>();

                if (category != null)
                {

                    return Ok(await _productService.AddAsync(product));
                }
                return BadRequest("Category is not found!");
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error: " + ex.Message);
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SaveWithCategoryName(ProductSaveWithCategoryNameDto productSaveWithCategoryNameDto)
        {

            Category category=new();
            try
            {
                //Categories API Consume
                string apiUrl = "https://localhost:7181/api/Categories/GetByName/" + productSaveWithCategoryNameDto.CategoryName;

                var response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {

                    var responseContent = await response.Content.ReadAsStringAsync();
                     category = await response.Content.ReadFromJsonAsync<Category>();
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    string apiUrlCategorySave = "https://localhost:7181/api/Categories/Save/";
                    CategoryDto categoryDto = new()
                    {
                        Name = productSaveWithCategoryNameDto.CategoryName
                    };
                    var jsonData = JsonConvert.SerializeObject(categoryDto);
                    var responseCategorySave = await _httpClient.PostAsync(apiUrlCategorySave, new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"));
                    if (responseCategorySave.IsSuccessStatusCode)
                    {

                        var responseContent = await responseCategorySave.Content.ReadAsStringAsync();
                         category = await responseCategorySave.Content.ReadFromJsonAsync<Category>();
                    }
                    else
                    {
                        return BadRequest("Fail!!! Http Code: " + responseCategorySave.StatusCode + " Error message: " + responseCategorySave.Content.ReadAsStringAsync().Result);
                    }
                }


                Product product = new()
                {
                    Name = productSaveWithCategoryNameDto.Name,
                    Description = productSaveWithCategoryNameDto.Description,
                    Price = productSaveWithCategoryNameDto.Price,
                    CategoryId = category.Id,
                };
                return Ok(await _productService.AddAsync(product));


            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error: " + ex.Message);
            }



        }


        //Get By Category Id method: Returns products belonging to a certain category.
        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            return Ok(await _productService.GetByCategoryIdAsync(categoryId));
        }

        //Kategori Adına Göre Al yöntemi: Kategori adına göre ürünleri getirir.
        [HttpGet("[action]/{categoryName}")]
        public async Task<IActionResult> GetByCategoryName(string categoryName)
        {
            try
            {
                //Categories API Consume
                string apiUrl = "https://localhost:7181/api/Categories/GetByName/" + categoryName;

                var response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return BadRequest("Fail!!! Http Code: " + response.StatusCode);
                }
                Category category = await response.Content.ReadFromJsonAsync<Category>();
                return Ok(await _productService.GetByCategoryIdAsync(category.Id));

            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error: " + ex.Message);
            }

        }
    }
}
