using Institutes_Managements.Database;
using Institutes_Managements.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Institutes_Managements.Controllers
{
    public class AdminController : Controller
    {
        private readonly InstituteDbcontext _context;

        public AdminController(InstituteDbcontext context)
        {
            _context = context;
        }

        // GET: Admin/Courses
        public async Task<IActionResult> Courses()
        {
            var courses = await _context.Courses.Include(c => c.Batches).ToListAsync();
            return View(courses);
        }

        // GET: Admin/CreateCourse
        public IActionResult CreateCourse()
        {
            return View();
        }

        // POST: Admin/CreateCourse
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Courses));
            }
            return View(course);
        }

        // GET: Admin/EditCourse/{id}
        public async Task<IActionResult> EditCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Admin/EditCourse
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Update(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Courses));
            }
            return View(course);
        }

        // POST: Admin/DeleteCourse/{id}
        [HttpPost]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Courses));
        }
    }
}
