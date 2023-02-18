using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System;
using Hanssens.Net;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using OnBoarding.Models;
using System.Net.Http.Headers;
using NuGet.Common;

namespace OnBoarding.Controllers
{
    public class AuthorizeController : Controller
    {
        LocalStorage storage = new LocalStorage();
        const string KEY = "JWTToken";

        /*private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("http://192.168.1.56:8080/api/v1/"),
        };*/

        private static HttpClient client = new HttpClient();
        private static HttpRequestMessage request  = new HttpRequestMessage()
        {
            RequestUri = new Uri("http://192.168.1.56:8080/api/v1/"),
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
                    email = email,
                    password = password
                }),
                Encoding.UTF8,
                "application/json");


            var req = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://192.168.1.56:8080/api/v1/"),
            };
            req.Method = HttpMethod.Post;
            req.RequestUri = new Uri(req.RequestUri.ToString() + "user/login");
            req.Content = jsonContent;
            //using HttpResponseMessage response = await sharedClient.PostAsync("user/login", jsonContent);
            using HttpResponseMessage response = await client.SendAsync(req);
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

            var req2 = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://192.168.1.56:8080/api/v1/"),
            };
            req2.RequestUri = new Uri(req2.RequestUri.ToString() + "test");
            req2.Method = HttpMethod.Get;
            req2.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2NzY4MDY0NzIsImlhdCI6MTY3NjcyMDA3MiwidXNlcl9ndWlkIjoiMzczNDAzYTQtYWY4MC0xMWVkLThhMDktMDI0MmFjMTMwMDAyIn0.W7h-Ef2k6bAmYqxzqpyjOrn2akgHIWXo_T4BHEV1-TA");
            using HttpResponseMessage response2 = await client.SendAsync(req2);
            Console.WriteLine(response2.EnsureSuccessStatusCode());

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Author(String email, String password)
        {

            String jsonResponse;
            try
            {
                var req = request;
                req.RequestUri = new Uri(req.RequestUri.ToString() + "/test");
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2NzY4MDY0NzIsImlhdCI6MTY3NjcyMDA3MiwidXNlcl9ndWlkIjoiMzczNDAzYTQtYWY4MC0xMWVkLThhMDktMDI0MmFjMTMwMDAyIn0.W7h-Ef2k6bAmYqxzqpyjOrn2akgHIWXo_T4BHEV1-TA");
                // using HttpResponseMessage response = await client.SendAsync(req);
                //Console.WriteLine(response.EnsureSuccessStatusCode());

                //jsonResponse = await response.Content.ReadAsStringAsync();

            }
            catch
            {
                return View();

            }


            var clientHandler = new HttpClientHandler();
            var client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2NzY4MDY0NzIsImlhdCI6MTY3NjcyMDA3MiwidXNlcl9ndWlkIjoiMzczNDAzYTQtYWY4MC0xMWVkLThhMDktMDI0MmFjMTMwMDAyIn0.W7h-Ef2k6bAmYqxzqpyjOrn2akgHIWXo_T4BHEV1-TA");
            //Console.WriteLine(sharedClient.ToString());
            return RedirectToAction("Index", "Home");
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
                   email = email,
                   password = password
               }),
               Encoding.UTF8,
               "application/json");
            String jsonResponse;
            try
            {
                //sing HttpResponseMessage response = await sharedClient.PostAsync("user/register", jsonRegisterContent);
                //Console.WriteLine(response.EnsureSuccessStatusCode());

                //jsonResponse = await response.Content.ReadAsStringAsync();

            }
            catch
            {
                ViewBag.Error = "error";
                return View();
                
            }

            

            //var jsonBody = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);
            //Console.WriteLine("JWT: " + jsonBody.JWT);
            //storage.Store(KEY, jsonBody.JWT);


            string localToken;
            if (storage.Exists(KEY))
            {
                localToken = storage.Get(KEY).ToString();
            }
            else
            {
                return View();
            }

            using (var requestMessage =
            new HttpRequestMessage(HttpMethod.Get, "http://192.168.1.56:8080/api/v1/"))
            {
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", localToken);

               // await sharedClient.SendAsync(requestMessage);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
