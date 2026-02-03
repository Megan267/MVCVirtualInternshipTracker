using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCVirtualInternshipTracker.Models;
using System.Threading.Tasks;

namespace MVCVirtualInternshipTracker.Controllers
{
    public class InternshipController : Controller
    {
        private readonly ApplicationDbContext _context;
        public InternshipController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var Internship = await _context.Internships.ToListAsync();

            return View(Internship);
        }

        //Get : Create
        public IActionResult Create()
        {
            //otherway than include
            ViewBag.Students = _context.Students.ToList();
            ViewBag.Companies = _context.Company.ToList();

            return View();
        }

        //POST: Create
        [HttpPost]
        public async Task<IActionResult> Create(Internship internship)
        {
            if (ModelState.IsValid)
            {
                _context.Add(internship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //viewbag here - if the model is not valid
            ViewBag.Students = _context.Students.ToList(); 
            ViewBag.Companies = _context.Company.ToList();
            return View(internship);
        }

        public async Task<IActionResult> LogHoursAsync(int id)
        {
            var internship = await _context.Internships.FindAsync(id);
            if (internship == null)
                return NotFound();
            return View(internship);

        }
       
        [HttpPost]
        public async Task<IActionResult> LogHours(int id, Internship model)
        {
            var internship = await _context.Internships.FindAsync(id);
            if (internship == null)
                return NotFound();

            internship.HoursLogged = model.HoursLogged;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Feedback(int id)
        {
            var internship = await _context.Internships.FindAsync(id);
            if (internship == null)
                return NotFound();
            return View(internship);

        }

        [HttpPost]
        public async Task<IActionResult> Feedback(int id, Internship model)
        {
            var internship = await _context.Internships.FindAsync(id);
            if (internship == null)
                return NotFound();

            internship.SupervisorFeedback = model.SupervisorFeedback;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
