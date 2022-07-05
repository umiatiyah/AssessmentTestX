using AssessmentTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace AssessmentTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        string Baseurl = "https://reqres.in/";
        public async Task<IActionResult> Index(UserModel user)
        {
            LoginDto EmpInfo = new LoginDto();
            using (var client = new HttpClient())
            {
                UserModel adm = new UserModel
                {
                    email = user.email,         //"eve.holt@reqres.in"
                    password = user.password    
                };
                var jsonauth = JsonSerializer.Serialize(adm);

                var content = new StringContent(jsonauth, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage Res = await client.PostAsync("api/login", content);
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    EmpInfo = JsonSerializer.Deserialize<LoginDto>(EmpResponse);
                    return View("Users");
                }
                return View();
            }
        }

        public async Task<IActionResult> GetUsers()
        {
            try
            {
                string Baseurl = "https://reqres.in/";

                ResponseDto EmpInfo = new ResponseDto();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    HttpResponseMessage Res = await client.GetAsync("api/users?page=1");
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        EmpInfo = JsonSerializer.Deserialize<ResponseDto>(EmpResponse);
                    }
                }
                return Ok(EmpInfo);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> DetailUser(int id)
        {
            try
            {
                string Baseurl = "https://reqres.in/";

                UserDetailDto EmpInfo = new UserDetailDto();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    HttpResponseMessage Res = await client.GetAsync("api/users/"+id);
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        EmpInfo = JsonSerializer.Deserialize<UserDetailDto>(EmpResponse);
                    }
                }
                return View(EmpInfo.data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
