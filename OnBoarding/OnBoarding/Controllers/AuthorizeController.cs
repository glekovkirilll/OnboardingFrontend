using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System;
using Hanssens.Net;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using OnBoarding.Models;

namespace OnBoarding.Controllers
{
    public class AuthorizeController : Controller
    {
        LocalStorage storage = new LocalStorage();
        const string KEY = "JWTToken";

        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("http://192.168.1.56:8080/api/v1/"),
        };

        [HttpGet]
        public ActionResult Auth()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Auth(String email, String password)
        {       

            using StringContent jsonContent = new(
            JsonConvert.SerializeObject(new
                {
                    email = "admin@example.com",
                    password = "admin"
                }),
                Encoding.UTF8,
                "application/json");


            using HttpResponseMessage response = await sharedClient.PostAsync("user/login", jsonContent);

            Console.WriteLine(response.EnsureSuccessStatusCode());

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var jsonBody = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);
            Console.WriteLine("JWT: " + jsonBody.JWT);
            storage.Store(KEY, jsonBody.JWT);


            string localToken;
            if (storage.Exists(KEY))
            {
                localToken = storage.Get(KEY).ToString();
            }
            else
            {
                return View("Auth");
            }


            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(String email, String password)
        {

            using StringContent jsonRegisterContent = new(
               JsonConvert.SerializeObject(new
               {
                   email = "admin1488@example.com",
                   password = "admin",
                   userName = "someasdasdasdadasdaseUserName"
               }),
               Encoding.UTF8,
               "application/json");

            using HttpResponseMessage response = await sharedClient.PostAsync("user/register", jsonRegisterContent);

            Console.WriteLine(response.EnsureSuccessStatusCode());

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var jsonBody = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);
            Console.WriteLine("JWT: " + jsonBody.JWT);
            storage.Store(KEY, jsonBody.JWT);


            string localToken;
            if (storage.Exists(KEY))
            {
                localToken = storage.Get(KEY).ToString();
            }
            else
            {
                return View("Auth");
            }

            return View("Auth");
        }
    }
}
