using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnBoarding.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using static OnBoarding.Controllers.AuthorizeController;

namespace OnBoarding.Controllers
{
    
    public class UsersController : Controller
    {
        //About about;
        private static HttpClient client = new HttpClient();
        private static HttpRequestMessage request = new HttpRequestMessage()
        {
            RequestUri = new Uri("http://192.168.1.56:8080/api/v1/"),
        };


        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("http://192.168.1.56:8080/api/v1/"),
        };
        public async Task<IActionResult> Index()
        {
            String jsonResponse;
            try
            {
                using HttpResponseMessage response = await sharedClient.GetAsync("user/all");
                Console.WriteLine(response.EnsureSuccessStatusCode());

                jsonResponse = await response.Content.ReadAsStringAsync();

            }
            catch
            {
                return View();

            }

            
            var jsonBody = JsonConvert.DeserializeObject<UserList>(jsonResponse);
            var model = jsonBody.users;
            Console.WriteLine(model);
            return View(model);
        }

        //public async Task<IActionResult> Details(int? id)
        //{
        //    String jsonResponse;
        //    try
        //    {
        //        using HttpResponseMessage response = await sharedClient.GetAsync("user/all");
        //        Console.WriteLine(response.EnsureSuccessStatusCode());

        //        jsonResponse = await response.Content.ReadAsStringAsync();

        //    }
        //    catch
        //    {
        //        return View();

        //    }


        //    var jsonBody = JsonConvert.DeserializeObject<UserList>(jsonResponse);
        //    var model = jsonBody.users;
        //    Console.WriteLine(model);
        //    return View(model);
        //}
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        // POST: Users/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string description, string fio, string contact)
        {
            using StringContent jsonContent = new(
            JsonConvert.SerializeObject(new
            {
                description = description,
                fio = fio,
                contact = contact
            }),
                Encoding.UTF8,
                "application/json");

            var req2 = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://192.168.1.56:8080/api/v1/"),
            };
            req2.RequestUri = new Uri(req2.RequestUri.ToString() + "user/about/update");
            req2.Method = HttpMethod.Get;
            req2.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Globals.JWTToken);
            req2.Content = jsonContent;
            using HttpResponseMessage response2 = await client.SendAsync(req2);

            var JsonResponse2 = await response2.Content.ReadAsStringAsync();
            var jsonBody2 = JsonConvert.DeserializeObject<UserInfoViewModel>(JsonResponse2);
            Console.WriteLine("FIO: " + jsonBody2.user);
            Globals.authorizedUser = jsonBody2.user;


            return RedirectToAction("Index", "Home"); 
        }

        public async Task<IActionResult> Leaderboard()
        {
            String jsonResponse;
            try
            {
                using HttpResponseMessage response = await sharedClient.GetAsync("user/all");
                Console.WriteLine(response.EnsureSuccessStatusCode());

                jsonResponse = await response.Content.ReadAsStringAsync();

            }
            catch
            {
                return View();

            }


            var jsonBody = JsonConvert.DeserializeObject<UserList>(jsonResponse);
            var model = jsonBody.users;
            Console.WriteLine(model);
            return View(model);
        }
    }
}
