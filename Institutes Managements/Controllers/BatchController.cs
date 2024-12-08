using Institutes_Managements.Database;
using Institutes_Managements.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Institutes_Managements.Controllers
{
    public class BatchController : Controller
    {
        private readonly InstituteDbcontext _context;

        public BatchController(InstituteDbcontext context)
        {
            _context = context;
        }

        // GET: Batch
        public async Task<IActionResult> Index()
        {
            var batches = await _context.Batchs.Include(b => b.Course).ToListAsync();
            return View(batches);
        }

        // GET: Batch/Create
        public IActionResult Create()
        {
            ViewBag.Courses = _context.Courses.ToList(); // For dropdown
            return View();
        }

        // POST: Batch/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Batch batch)
        {
            if (ModelState.IsValid)
            {
                _context.Batchs.Add(batch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Courses = _context.Courses.ToList();
            return View(batch);
        }

        // GET: Batch/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var batch = await _context.Batchs.FindAsync(id);
            if (batch == null)
            {
                return NotFound();
            }

            ViewBag.Courses = _context.Courses.ToList(); // For dropdown
            return View(batch);
        }

        // POST: Batch/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Batch batch)
        {
            if (id != batch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(batch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BatchExists(batch.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Courses = _context.Courses.ToList();
            return View(batch);
        }

        // GET: Batch/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var batch = await _context.Batchs
                .Include(b => b.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (batch == null)
            {
                return NotFound();
            }

            return View(batch);
        }

        // POST: Batch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var batch = await _context.Batchs.FindAsync(id);
            _context.Batchs.Remove(batch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BatchExists(int id)
        {
            return _context.Batchs.Any(e => e.Id == id);
        }
    }
}
