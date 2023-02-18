using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnBoarding.Data;
using OnBoarding.Models;

namespace OnBoarding.Controllers
{
    public class SubdivisionsController : Controller
    {
        //private readonly OnBoardingContext _context;
        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("http://192.168.1.56:8080/api/v1/"),
        };

        public SubdivisionsController(OnBoardingContext context)
        {
            //_context = context;
        }

        // GET: Subdivisions
        public async Task<IActionResult> Index()
        {
            String jsonResponse;
            try
            {
                using HttpResponseMessage response = await sharedClient.GetAsync("division/all");
                Console.WriteLine(response.EnsureSuccessStatusCode());

                jsonResponse = await response.Content.ReadAsStringAsync();

            }
            catch
            {
                return View();

            }


            var jsonBody = JsonConvert.DeserializeObject<DivisionsList>(jsonResponse);
            var model = jsonBody.divisions;
            Console.WriteLine(model);
            return View(model);
            
        }

        // GET: Subdivisions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (id == null || _context.Subdivision == null)
            //{
            //    return NotFound();
            //}

            //var subdivision = await _context.Subdivision
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (subdivision == null)
            //{
            //    return NotFound();
            //}

            return View(/*subdivision*/);
        }

        // GET: Subdivisions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subdivisions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Occupation")] Subdivision subdivision)
        {
           using StringContent jsonContent = new(
           JsonConvert.SerializeObject(new
           {
               description = subdivision.Description,
               name = subdivision.Name
           }),
               Encoding.UTF8,
               "application/json");

            String jsonResponse;
            try
            {
                using HttpResponseMessage response = await sharedClient.PostAsync("division/create", jsonContent);
                Console.WriteLine(response.EnsureSuccessStatusCode());

                jsonResponse = await response.Content.ReadAsStringAsync();

            }
            catch
            {
                return View();

            }

            var jsonBody = JsonConvert.DeserializeObject<object>(jsonResponse);
            Console.WriteLine(jsonBody);


            //var jsonBody = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);
            //Console.WriteLine("JWT: " + jsonBody.JWT);
            return View();
        }

        // GET: Subdivisions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null || _context.Subdivision == null)
            //{
            //    return NotFound();
            //}

            //var subdivision = await _context.Subdivision.FindAsync(id);
            //if (subdivision == null)
            //{
            //    return NotFound();
            //}
            return View();
        }

        // POST: Subdivisions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Occupation")] Subdivision subdivision)
        {
            //if (id != subdivision.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(subdivision);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!SubdivisionExists(subdivision.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            return View(/*subdivision*/);
        }

        // GET: Subdivisions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id == null || _context.Subdivision == null)
            //{
            //    return NotFound();
            //}

            //var subdivision = await _context.Subdivision
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (subdivision == null)
            //{
            //    return NotFound();
            //}

            return View(/*subdivision*/);
        }

        // POST: Subdivisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //if (_context.Subdivision == null)
            //{
            //    return Problem("Entity set 'OnBoardingContext.Subdivision'  is null.");
            //}
            //var subdivision = await _context.Subdivision.FindAsync(id);
            //if (subdivision != null)
            //{
            //    _context.Subdivision.Remove(subdivision);
            //}
            
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool SubdivisionExists(int id)
        //{
        //  return _context.Subdivision.Any(e => e.Id == id);
        //}
    }
}
