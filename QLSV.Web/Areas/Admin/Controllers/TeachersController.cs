using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLSV.Data.Infrastructure;
using QLSV.Model.Models;

namespace QLSV.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeachersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeachersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/Teachers
        public IActionResult Index()
        {
            var students = _unitOfWork.TeacherRepos.GetAll();
            if (students != null)
                return View(students);

            return View();
        }

        // GET: Admin/Teachers/Details/5
        public IActionResult Details(int id)
        {
            if (id == null || _unitOfWork.StudentRepos == null)
            {
                return NotFound();
            }

            var student = _unitOfWork.StudentRepos.GetSingleById(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Admin/Teachers/Create
        public IActionResult Create()
        {
            //ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
            return View();
        }

        // POST: Admin/Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.TeacherRepos.Add(teacher);
                _unitOfWork.SaveChange();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", teacher.DepartmentId);
            return View(teacher);
        }

        // GET: Admin/Teachers/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = _unitOfWork.StudentRepos.GetSingleById(id);
            if (teacher == null)
            {
                return NotFound();
            }
            //ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", teacher.DepartmentId);
            return View(teacher);
        }

        // POST: Admin/Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Teacher teacher)
        {
            if (id != teacher.TeacherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.TeacherRepos.Update(teacher);
                    _unitOfWork.SaveChange();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();                   
                }
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Admin/Teachers/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = _unitOfWork.TeacherRepos.GetSingleById(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Admin/Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var teacher = _unitOfWork.TeacherRepos.GetSingleById(id);
            if (teacher != null)
            {
                _unitOfWork.TeacherRepos.Delete(teacher);
            }

            _unitOfWork.SaveChange();
            return RedirectToAction(nameof(Index));
        }
    }
}
