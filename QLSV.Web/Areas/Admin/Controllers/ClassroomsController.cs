using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLSV.Common;
using QLSV.Data;
using QLSV.Model.Models;

namespace QLSV.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RolesHelper.Role_Admin)]
    public class ClassroomsController : Controller
    {
        private readonly StudentDBContext _context;

        public ClassroomsController(StudentDBContext context)
        {
            _context = context;
        }

        // GET: Admin/Classrooms
        public async Task<IActionResult> Index()
        {
            var studentDBContext = _context.Classrooms.Include(c => c.Course).Include(c => c.Teacher);
            return View(await studentDBContext.ToListAsync());
        }

        // GET: Admin/Classrooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Classrooms == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classrooms
                .Include(c => c.Course)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.ClassroomId == id);
            if (classroom == null)
            {
                return NotFound();
            }

            return View(classroom);
        }

        // GET: Admin/Classrooms/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Name");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "Name");
            return View();
        }

        // POST: Admin/Classrooms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Classroom classroom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classroom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Name", classroom.CourseId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "Name", classroom.TeacherId);
            return View(classroom);
        }

        // GET: Admin/Classrooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Classrooms == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Name", classroom.CourseId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "Name", classroom.TeacherId);
            return View(classroom);
        }

        // POST: Admin/Classrooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Classroom classroom)
        {
            if (id != classroom.ClassroomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classroom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassroomExists(classroom.ClassroomId))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Name", classroom.CourseId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "Name", classroom.TeacherId);
            return View(classroom);
        }

        // GET: Admin/Classrooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Classrooms == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classrooms
                .Include(c => c.Course)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.ClassroomId == id);
            if (classroom == null)
            {
                return NotFound();
            }

            return View(classroom);
        }

        // POST: Admin/Classrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Classrooms == null)
            {
                return Problem("Entity set 'StudentDBContext.Classrooms'  is null.");
            }
            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom != null)
            {
                _context.Classrooms.Remove(classroom);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassroomExists(int id)
        {
          return (_context.Classrooms?.Any(e => e.ClassroomId == id)).GetValueOrDefault();
        }
    }
}
