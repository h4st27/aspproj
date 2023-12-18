using lr12.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace lr12.Controllers
{
    public class CompanyController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        ApplicationContext db;
        public CompanyController(ApplicationContext context, ILogger<HomeController> logger)
        {
            db = context;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Companies.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Create(CompanyModel company)
        {
            db.Companies.Add(company);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                CompanyModel company = new CompanyModel { Id = id.Value };
                db.Entry(company).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CompanyModel company)
        {
            db.Companies.Update(company);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                CompanyModel? company = await db.Companies.FirstOrDefaultAsync(p => p.Id == id);
                if (company != null) return View(company);
            }
            return NotFound();
        }
    }
}
