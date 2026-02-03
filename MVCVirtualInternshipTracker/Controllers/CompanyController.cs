using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCVirtualInternshipTracker.Models;

namespace MVCVirtualInternshipTracker.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var Company = await _context.Company.ToListAsync();
            return View(Company);
        }

        public async Task<IActionResult> Details(int id)
        {
            var company = await _context.Company.
                Include(c => c.Internships)
                .ThenInclude(i => i.Student)
                .FirstOrDefaultAsync(c => c.Id == id);

            if(company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        private bool CompanyExists(int id)
        {
            return _context.Company.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Edit(int? id) // ? means it can take null, ignore the data type
        {
            if(id == null)
            {
                return NotFound();
            }

            var company = await _context.Company.FindAsync(id);
            if(company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Company company)
        {
                if(id != company.Id)
                {
                    return NotFound();
                }

                if(ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(company);
                        await _context.SaveChangesAsync();
                    }
                    catch(DbUpdateConcurrencyException)
                    {
                        if(!CompanyExists(company.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            //throw stops an error and stops the program in its track
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(company);
        }

        public async Task<IActionResult> Delete(int? id) // sir left something out in this method for the assignment, we have to find it and do it
        {
            //The 2 already in the database sql cannot be deleted - that is thing left out and we have to figure it out
            //like have to get rid of internships related to the company to delete the company (something like this)
            if(id == null)
            {
                return NotFound();
            }
            var company = await _context.Company.FirstOrDefaultAsync(c => c.Id == id);
            if(company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var company = await _context.Company.FindAsync(id);
            if(company == null)
            {
                return NotFound();
            }
            _context.Company.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
