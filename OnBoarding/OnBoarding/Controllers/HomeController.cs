using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnBoarding.Models;
using System.Diagnostics;

namespace OnBoarding.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("http://192.168.1.56:8080/api/v1/"),
        };

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LogInAsync()
        {
            return View();
        }

        public async Task<IActionResult> Quests()
        {
            String jsonResponse;
            String jsonResponseQuest;
            try
            {
                using HttpResponseMessage response = await sharedClient.GetAsync("division/all");
                Console.WriteLine(response.EnsureSuccessStatusCode());

                jsonResponse = await response.Content.ReadAsStringAsync();

                using HttpResponseMessage responseUsers = await sharedClient.GetAsync("quest/all");
                Console.WriteLine(responseUsers.EnsureSuccessStatusCode());

                jsonResponseQuest = await responseUsers.Content.ReadAsStringAsync();
            }
            catch
            {
                return View();

            }


            var jsonBody = JsonConvert.DeserializeObject<DivisionsList>(jsonResponse);

            var jsonUsersBody = JsonConvert.DeserializeObject<QuestList>(jsonResponseQuest);

            var model = new DivisionsAndQuests()
            {
                Divisions = jsonBody,
                Quests = jsonUsersBody

            };

            Console.WriteLine(model);


            return View(model);
           
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}