using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnBoarding.Models;

namespace OnBoarding.Controllers
{
    public class LeaderBoardController : Controller
    {
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
    }
}
