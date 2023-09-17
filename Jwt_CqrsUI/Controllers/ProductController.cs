using Jwt_CqrsUI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Jwt_CqrsUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            
            var token = User.Claims.FirstOrDefault(x=>x.Type == "accessToken").Value;
            if(token != null)
            {
                var client = this.httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
               var response = await client.GetAsync("http://localhost:5020/api/Product/GetProducts");;
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<List<ProductModel>>(jsonData, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    return View(result);
                }
              
            }
           

            return View();
        }
        public async Task<IActionResult> Remove(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
            if (token!=null)
            {
               var client = this.httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue
                (
                    JwtBearerDefaults.AuthenticationScheme,
                    token
                );
                await client.GetAsync($"http://localhost:5020/api/Product/{id}");
            }
            return RedirectToAction("Index","Product");
        }
 
    }
}
