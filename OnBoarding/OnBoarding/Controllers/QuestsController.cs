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
    public class QuestsController : Controller
    {
        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("http://192.168.1.56:8080/api/v1/"),
        };

        public QuestsController(OnBoardingContext context)
        {
            //_context = context;
        }

        // GET: Quests
        public async Task<IActionResult> Index()
        {
            String jsonResponse;
            String jsonResponseDivisions;
            try
            {
                using HttpResponseMessage response = await sharedClient.GetAsync("quest/all");
                Console.WriteLine(response.EnsureSuccessStatusCode());

                jsonResponse = await response.Content.ReadAsStringAsync();

                using HttpResponseMessage responseDivision = await sharedClient.GetAsync("division/all");
                Console.WriteLine(responseDivision.EnsureSuccessStatusCode());

                jsonResponseDivisions = await responseDivision.Content.ReadAsStringAsync();

            }
            catch
            {
                return View();

            }


            var jsonBody = JsonConvert.DeserializeObject<QuestList>(jsonResponse);

            var jsonBodyDivisions = JsonConvert.DeserializeObject<DivisionsList>(jsonResponseDivisions);

            var model = new QuestDivision()
            {
                Quests = jsonBody.quests,
                Subdivisions = jsonBodyDivisions.divisions

            };
            Console.WriteLine(model);


            return View(model);
        }

        // GET: Quests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (id == null || _context.Quest == null)
            //{
            //    return NotFound();
            //}

            //var quest = await _context.Quest
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (quest == null)
            //{
            //    return NotFound();
            //}

            return View(/*quest*/);
        }

       
        // POST: Quests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DivisionId,Name,Description")] CreateQuest quest)
        {
            using StringContent jsonContent = new(
           JsonConvert.SerializeObject(new
           {
               description = quest.Description,
               divisionID = quest.DivisionId,
               name = quest.Name
               
           }),
               Encoding.UTF8,
               "application/json");

            String jsonResponse;
            try
            {
                using HttpResponseMessage response = await sharedClient.PostAsync("quest/add", jsonContent);
                Console.WriteLine(response.EnsureSuccessStatusCode());

                jsonResponse = await response.Content.ReadAsStringAsync();

            }
            catch
            {
                return RedirectToAction(nameof(Index));

            }

            var jsonBody = JsonConvert.DeserializeObject<object>(jsonResponse);
            Console.WriteLine(jsonBody);


            //var jsonBody = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);
            //Console.WriteLine("JWT: " + jsonBody.JWT);
            return RedirectToAction(nameof(Index));
        }

        // GET: Quests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Quest quest)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(quest);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            return View(/*quest*/);
        }

        // GET: Quests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null || _context.Quest == null)
            //{
            //    return NotFound();
            //}

            //var quest = await _context.Quest.FindAsync(id);
            //if (quest == null)
            //{
            //    return NotFound();
            //}
            return View(/*quest*/);
        }

        // POST: Quests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Quest quest)
        {
            //if (id != quest.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(quest);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!QuestExists(quest.Id))
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
            return View(/*quest*/);
        }

        // GET: Quests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
        //    if (id == null || _context.Quest == null)
        //    {
        //        return NotFound();
        //    }

        //    var quest = await _context.Quest
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (quest == null)
        //    {
        //        return NotFound();
        //    }

            return View(/*quest*/);
        }

        // POST: Quests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //if (_context.Quest == null)
            //{
            //    return Problem("Entity set 'OnBoardingContext.Quest'  is null.");
            //}
            //var quest = await _context.Quest.FindAsync(id);
            //if (quest != null)
            //{
            //    _context.Quest.Remove(quest);
            //}
            
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool QuestExists(int id)
        //{
        //  return _context.Quest.Any(e => e.Id == id);
        //}
    }
}
