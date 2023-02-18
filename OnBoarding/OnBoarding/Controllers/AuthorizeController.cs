using Microsoft.AspNetCore.Mvc;
using Hanssens.Net;
using Newtonsoft.Json;
using System.Text;
using OnBoarding.Models;
using System.Net.Http.Headers;


namespace OnBoarding.Controllers
{
    public class AuthorizeController : Controller
    {
        LocalStorage storage = new LocalStorage();
        const string KEY = "JWTToken";

        private static HttpClient client = new HttpClient();
        private static HttpRequestMessage request = new HttpRequestMessage()
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
            // Data model for login
            using StringContent jsonContent = new(
            JsonConvert.SerializeObject(new
            {
                email = email,
                password = password
            }),
                Encoding.UTF8,
                "application/json");

            // Post data model to server
            var req = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://192.168.1.56:8080/api/v1/"),
            };
            req.Method = HttpMethod.Post;
            req.RequestUri = new Uri(req.RequestUri.ToString() + "user/login");
            req.Content = jsonContent;
            using HttpResponseMessage response = await client.SendAsync(req);

            // Write return code for data
            Console.WriteLine(response.EnsureSuccessStatusCode());

            // Get JWT token
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonBody = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);
            Console.WriteLine("JWT: " + jsonBody.JWT);

            // Add JWT to local storage
            storage.Store(KEY, jsonBody.JWT);

            // Verification and localization for token
            string localToken;
            if (storage.Exists(KEY))
            {
                localToken = storage.Get(KEY).ToString();
            }
            else
            {
                return View("Auth");
            }

            // Post JWT to header
            var req2 = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://192.168.1.56:8080/api/v1/"),
            };
            req2.RequestUri = new Uri(req2.RequestUri.ToString() + "test");
            req2.Method = HttpMethod.Get;
            req2.Headers.Authorization = new AuthenticationHeaderValue("Bearer", localToken);
            using HttpResponseMessage response2 = await client.SendAsync(req2);

            // Write return code for JWT
            Console.WriteLine(response2.EnsureSuccessStatusCode());

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
            // Data model for registration
            using StringContent jsonRegisterContent = new(
               JsonConvert.SerializeObject(new
               {
                   email = email,
                   password = password
               }),
               Encoding.UTF8,
               "application/json");


            // Post data model to server
            var req = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://192.168.1.56:8080/api/v1/"),
            };
            req.Method = HttpMethod.Post;
            req.RequestUri = new Uri(req.RequestUri.ToString() + "user/register");
            req.Content = jsonRegisterContent;
            using HttpResponseMessage response = await client.SendAsync(req);

            // Write return code for data
            Console.WriteLine(response.EnsureSuccessStatusCode());

            // Get JWT token
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonBody = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);
            Console.WriteLine("JWT: " + jsonBody.JWT);

            // Add JWT to local storage
            storage.Store(KEY, jsonBody.JWT);

            // Verification and localization for token
            string localToken;
            if (storage.Exists(KEY))
            {
                localToken = storage.Get(KEY).ToString();
            }
            else
            {
                return View("Index", "Home");
            }

            // Post JWT to header
            var req2 = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://192.168.1.56:8080/api/v1/"),
            };
            req2.RequestUri = new Uri(req2.RequestUri.ToString() + "test");
            req2.Method = HttpMethod.Get;
            req2.Headers.Authorization = new AuthenticationHeaderValue("Bearer", localToken);
            using HttpResponseMessage response2 = await client.SendAsync(req2);

            // Write return code for JWT
            Console.WriteLine(response2.EnsureSuccessStatusCode());

            return RedirectToAction("Index", "Home");
        }
    }
}
