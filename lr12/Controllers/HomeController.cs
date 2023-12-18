using lr12.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace lr12.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        ApplicationContext db;
        public HomeController(ApplicationContext context, ILogger<HomeController> logger)
        {
            db = context;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var users = await db.Users.ToListAsync();
            await Console.Out.WriteLineAsync("\n");
            foreach (var user in users)
            {
                Console.WriteLine($"{user.Name} {user.Surname} {user.Age}");
            }
            await Console.Out.WriteLineAsync("\n");
            return View(await db.Users.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserModel user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult>Delete(int? id)
        {
            if(id != null)
            {
                UserModel user = new UserModel {  Id = id.Value };
                db.Entry(user).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserModel user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id != null)
            {
                UserModel? user = await db.Users.FirstOrDefaultAsync(p =>  p.Id == id);
                if (user != null) return View(user);
            }
            return NotFound();
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
