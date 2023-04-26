using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLSV.Data;
using QLSV.Model.Models;

namespace QLSV.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ResultsController : Controller
    {
        private readonly StudentDBContext _context;

        public ResultsController(StudentDBContext context)
        {
            _context = context;
        }

        // GET: Admin/Results
        public async Task<IActionResult> Index()
        {
            var studentDBContext = _context.Results.Include(r => r.Classroom).Include(r => r.Student);
            return View(await studentDBContext.ToListAsync());
        }

        // GET: Admin/Results/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Results == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .Include(r => r.Classroom)
                .Include(r => r.Student)
                .FirstOrDefaultAsync(m => m.TeacherId == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Admin/Results/Create
        public IActionResult Create()
        {
            ViewData["TeacherId"] = new SelectList(_context.Classrooms, "TeacherId", "Lesson");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "CCCDNumber");
            return View();
        }

        // POST: Admin/Results/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,TeacherId,CourseId,RegularMark,MitermMark,FinalMark,Note")] Result result)
        {
            if (ModelState.IsValid)
            {
                _context.Add(result);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeacherId"] = new SelectList(_context.Classrooms, "TeacherId", "Lesson", result.TeacherId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "CCCDNumber", result.StudentId);
            return View(result);
        }

        // GET: Admin/Results/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Results == null)
            {
                return NotFound();
            }

            var result = await _context.Results.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            ViewData["TeacherId"] = new SelectList(_context.Classrooms, "TeacherId", "Lesson", result.TeacherId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "CCCDNumber", result.StudentId);
            return View(result);
        }

        // POST: Admin/Results/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,TeacherId,CourseId,RegularMark,MitermMark,FinalMark,Note")] Result result)
        {
            if (id != result.TeacherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(result);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultExists(result.TeacherId))
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
            ViewData["TeacherId"] = new SelectList(_context.Classrooms, "TeacherId", "Lesson", result.TeacherId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "CCCDNumber", result.StudentId);
            return View(result);
        }

        // GET: Admin/Results/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Results == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .Include(r => r.Classroom)
                .Include(r => r.Student)
                .FirstOrDefaultAsync(m => m.TeacherId == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Admin/Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Results == null)
            {
                return Problem("Entity set 'StudentDBContext.Results'  is null.");
            }
            var result = await _context.Results.FindAsync(id);
            if (result != null)
            {
                _context.Results.Remove(result);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultExists(int id)
        {
          return (_context.Results?.Any(e => e.TeacherId == id)).GetValueOrDefault();
        }
    }
}
