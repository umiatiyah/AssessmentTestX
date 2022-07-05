using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssessmentTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {
        }
        [HttpGet]
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

                var customerData = EmpInfo;
                return Ok(customerData);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}