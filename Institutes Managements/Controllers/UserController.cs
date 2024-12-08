using Institutes_Managements.Database;
using Institutes_Managements.Models.Entities;
using Institutes_Managements.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Institutes_Managements.Controllers
{
    public class UserController : Controller
    {
        private readonly InstituteDbcontext _context;

        public UserController(InstituteDbcontext context)
        {
            _context = context;
        }

        // GET: User/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                                          .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    HttpContext.Session.SetString("Role", user.Role.ToLower());
                    HttpContext.Session.SetString("UserEmail", user.Email);

                    return RedirectToAction(nameof(Dashboard));
                }

                ModelState.AddModelError("", "Invalid email or password.");
            }

            return View(model);
        }

        // POST: User/Logout
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        // GET: User/Dashboard
        public IActionResult Dashboard()
        {
            var role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(role))
            {
                return RedirectToAction(nameof(Login));
            }

            return role switch
            {
                "admin" => View("AdminDashboard"),
                "instructor" => View("InstructorDashboard"),
                "student" => View("StudentDashboard"),
                _ => RedirectToAction(nameof(Login))
            };
        }

        // GET: User/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users
                                                  .FirstOrDefaultAsync(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Email is already in use.");
                    return View(model);
                }

                var user = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Password = model.Password,
                    Role = model.Role.ToLower(),
                    ContactNumber = model.ContactNumber,
                    DateOfBirth = model.DateOfBirth,
                    Address = model.Address
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Login));
            }

            return View(model);
        }

        // GET: Assign Instructor to Batch
        public IActionResult AssignInstructorToBatch(int batchId)
        {
            var batch = _context.Batchs
                .Include(b => b.Course)
                .FirstOrDefault(b => b.Id == batchId);

            if (batch == null)
            {
                return NotFound("Batch not found.");
            }

            var instructors = _context.Users
                .Where(u => u.Role.ToLower() == "instructor")
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.FullName
                }).ToList();

            ViewData["Instructors"] = instructors;
            ViewData["Batch"] = batch;

            return View();
        }

        // POST: Assign Instructor to Batch
        [HttpPost]
        public async Task<IActionResult> AssignInstructorToBatch(int batchId, int instructorId)
        {
            var batch = await _context.Batchs
                .Include(b => b.Instructors)
                .FirstOrDefaultAsync(b => b.Id == batchId);

            if (batch == null)
            {
                return NotFound("Batch not found.");
            }

            var instructor = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == instructorId && u.Role.ToLower() == "instructor");

            if (instructor == null)
            {
                return NotFound("Instructor not found.");
            }

            batch.Instructors ??= new List<User>();

            if (!batch.Instructors.Contains(instructor))
            {
                batch.Instructors.Add(instructor);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Dashboard));
        }

        // GET: Enroll Student in Batch
        public IActionResult EnrollStudentInBatch()
        {
            var batches = _context.Batchs
                .Include(b => b.Course)
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = $"{b.BatchName} ({b.Course.CourseName})"
                }).ToList();

            ViewData["Batches"] = batches;

            var students = _context.Users
                .Where(u => u.Role.ToLower() == "student")
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.FullName
                }).ToList();

            ViewData["Students"] = students;

            return View();
        }

        // POST: Enroll Student in Batch
        [HttpPost]
        public async Task<IActionResult> EnrollStudentInBatch(int batchId, int studentId)
        {
            var batch = await _context.Batchs
                .Include(b => b.Students)
                .FirstOrDefaultAsync(b => b.Id == batchId);

            if (batch == null)
            {
                return NotFound("Batch not found.");
            }

            var student = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == studentId && u.Role.ToLower() == "student");

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            batch.Students ??= new List<User>();

            if (!batch.Students.Contains(student))
            {
                batch.Students.Add(student);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Dashboard));
        }
    }
}
