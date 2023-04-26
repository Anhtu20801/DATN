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
    public class PrimaryClassesController : Controller
    {
        private readonly StudentDBContext _context;

        public PrimaryClassesController(StudentDBContext context)
        {
            _context = context;
        }

        // GET: Admin/PrimaryClasses
        public async Task<IActionResult> Index()
        {
            var studentDBContext = _context.PrimaryClasses.Include(p => p.Department);
            return View(await studentDBContext.ToListAsync());
        }

        // GET: Admin/PrimaryClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PrimaryClasses == null)
            {
                return NotFound();
            }

            var primaryClass = await _context.PrimaryClasses
                .Include(p => p.Department)
                .FirstOrDefaultAsync(m => m.PrimaryClassId == id);
            if (primaryClass == null)
            {
                return NotFound();
            }

            return View(primaryClass);
        }

        // GET: Admin/PrimaryClasses/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
            return View();
        }

        // POST: Admin/PrimaryClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrimaryClassId,Name,Description,DepartmentId")] PrimaryClass primaryClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(primaryClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", primaryClass.DepartmentId);
            return View(primaryClass);
        }

        // GET: Admin/PrimaryClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PrimaryClasses == null)
            {
                return NotFound();
            }

            var primaryClass = await _context.PrimaryClasses.FindAsync(id);
            if (primaryClass == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", primaryClass.DepartmentId);
            return View(primaryClass);
        }

        // POST: Admin/PrimaryClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrimaryClassId,Name,Description,DepartmentId")] PrimaryClass primaryClass)
        {
            if (id != primaryClass.PrimaryClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(primaryClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrimaryClassExists(primaryClass.PrimaryClassId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", primaryClass.DepartmentId);
            return View(primaryClass);
        }

        // GET: Admin/PrimaryClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PrimaryClasses == null)
            {
                return NotFound();
            }

            var primaryClass = await _context.PrimaryClasses
                .Include(p => p.Department)
                .FirstOrDefaultAsync(m => m.PrimaryClassId == id);
            if (primaryClass == null)
            {
                return NotFound();
            }

            return View(primaryClass);
        }

        // POST: Admin/PrimaryClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PrimaryClasses == null)
            {
                return Problem("Entity set 'StudentDBContext.PrimaryClasses'  is null.");
            }
            var primaryClass = await _context.PrimaryClasses.FindAsync(id);
            if (primaryClass != null)
            {
                _context.PrimaryClasses.Remove(primaryClass);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrimaryClassExists(int id)
        {
          return (_context.PrimaryClasses?.Any(e => e.PrimaryClassId == id)).GetValueOrDefault();
        }
    }
}
