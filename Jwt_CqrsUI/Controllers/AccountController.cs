using Jwt_CqrsUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Jwt_CqrsUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            if(ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var content = new StringContent(JsonSerializer.Serialize(model),Encoding.UTF8,"application/json");

                var response = await client.PostAsync("http://localhost:5020/api/Auth/Login", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonData =await response.Content.ReadAsStringAsync();
                    var tokenModel = JsonSerializer.Deserialize<JwtTokenResponseModel>(jsonData, new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive= true
                    });

                    if(tokenModel != null)
                    {
                        JwtSecurityTokenHandler tokenHandler = new();
                        var token = tokenHandler.ReadJwtToken(tokenModel.Token);

                        var claimss = token.Claims.ToList();
                        if (tokenModel.Token !=null)
                        {
                            claimss.Add(new Claim("accessToken", tokenModel.Token));
                        }
                        var claimsIdentity = new ClaimsIdentity(claimss, JwtBearerDefaults.AuthenticationScheme);
                        var claims = new ClaimsPrincipal(claimsIdentity);

                        var authProps = new AuthenticationProperties()
                        {
                            ExpiresUtc = tokenModel.ExpireDate,
                            IsPersistent = true
                        };
                        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, claims, authProps);
                        return RedirectToAction("Index","Home");
                    }
                   
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Hatalı");
                }
            }
            return View(model);
        }
    }
}
