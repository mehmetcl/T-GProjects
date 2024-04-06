using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using T_GProjects.EntityLayer.Concrete;
using T_GProjects.MVC.Models;

namespace T_GProjects.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;

        public HomeController(ILogger<HomeController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _memoryCache = memoryCache;
        }
        //Categories API Consume
        public async Task<IActionResult> Index()
        {
          
            try
            {
                if (!_memoryCache.TryGetValue("Categories", out List<Category> categories))
                {
                    string apiUrl = "https://localhost:7181/api/Categories/GetAll/";

                    var response = await _httpClient.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return BadRequest("Fail! Http Code: " + response.StatusCode);
                    }

                    List<Category> categoryList = await response.Content.ReadFromJsonAsync<List<Category>>();
                    #region in memory cache
                    //In memory Cache
                    MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions();
                    cacheOptions.AbsoluteExpiration=DateTime.Now.AddSeconds(20);//waiting for 20 second
                    _memoryCache.Set<List<Category>>("Categories", categoryList,cacheOptions);
                    #endregion

                    return View(categoryList);
                }
                else
                {
                    return View(categories);
                }
               

            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error: " + ex.Message);
            }

        }

     
    }
}
