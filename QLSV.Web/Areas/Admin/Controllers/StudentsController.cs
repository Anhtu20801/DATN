using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLSV.Data.Infrastructure;

namespace QLSV.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/Students
        public IActionResult Index()
        {
            var students = _unitOfWork.StudentRepos.GetAll();
            if (students != null)
                return View(students);

            return View();
        }

        // GET: Admin/Students/Details/5
        public async Task<IActionResult> Details(int id)
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

        // GET: Admin/Students/Create
        public IActionResult Create()
        {
            ViewBag.ClassPrimaryList = _unitOfWork.PrimaryClassRepos.GetAll().Select(clp => new SelectListItem
            {
                Text = clp.Name,
                Value = clp.PrimaryClassId.ToString()
            });
            return View();
        }

        // POST: Admin/Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Model.Models.Student student)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.StudentRepos.Add(student);
                _unitOfWork.SaveChange();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ClassPrimaryList = _unitOfWork.PrimaryClassRepos.GetAll().Select(clp => new SelectListItem
            {
                Text = clp.Name,
                Value = clp.PrimaryClassId.ToString()
            });
            return View(student);
        }

        // GET: Admin/Students/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _unitOfWork.StudentRepos.GetSingleById(id);
            ViewBag.ClassPrimaryList = _unitOfWork.PrimaryClassRepos.GetAll().Select(clp => new SelectListItem
            {
                Text = clp.Name,
                Value = clp.PrimaryClassId.ToString()
            });
            if (student == null)
            {
                return NotFound();
            }
            //ViewData["PrimaryClassId"] = new SelectList(_unitOfWork.PrimaryClasses, "PrimaryClassId", "Name", student.PrimaryClassId);
            return View(student);
        }

        // POST: Admin/Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Model.Models.Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.StudentRepos.Update(student);
                    _unitOfWork.SaveChange();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ClassPrimaryList = _unitOfWork.PrimaryClassRepos.GetAll().Select(clp => new SelectListItem
            {
                Text = clp.Name,
                Value = clp.PrimaryClassId.ToString()
            });
            return View(student);
        }

        // GET: Admin/Students/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
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

        // POST: Admin/Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_unitOfWork.StudentRepos == null)
            {
                return Problem("Entity set '_unitOfWork.StudentRepos'  is null.");
            }
            var student = _unitOfWork.StudentRepos.GetSingleById(id);
            if (student != null)
            {
                _unitOfWork.StudentRepos.Delete(student);
            }
            
            _unitOfWork.SaveChange();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
