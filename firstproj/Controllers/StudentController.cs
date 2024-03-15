using System.Linq;
using firstproj.Data;
using firstproj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace firstproj.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var students = _dbContext.Students.ToList();
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                var existingEmailStudent = _dbContext.Students.FirstOrDefault(s => s.Email == student.Email);
                if (existingEmailStudent != null)
                {
                    ModelState.AddModelError("Email", "This email address is already in use.");
                    return View(student);
                }

                var existingRollNumberStudent = _dbContext.Students.FirstOrDefault(s => s.RollNO == student.RollNO);
                if (existingRollNumberStudent != null)
                {
                    ModelState.AddModelError("RollNO", "This roll number is already in use.");
                    return View(student);
                }

                _dbContext.Students.Add(student);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(student);
        }

        public IActionResult ResetCounter()
        {
            _dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE Students");
            _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Students', RESEED, 0)");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var student = _dbContext.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(int id, Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(student);
                    _dbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dbContext.Students.Any(s => s.Id == id))
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
            return View(student);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var student = _dbContext.Students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                _dbContext.Students.Remove(student);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
