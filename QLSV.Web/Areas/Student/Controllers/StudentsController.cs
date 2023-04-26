using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLSV.Data.Infrastructure;

namespace QLSV.Web.Areas.Student.Controllers
{
    [Area("Student")]
    public class StudentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Student/Students
        public IActionResult Index()
        {
            var students = _unitOfWork.StudentRepos.GetAll();
            if (students != null)
                return View(students);

            return View();
        }

        // GET: Student/Students/Details/5
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
        public IActionResult Edit(int id)
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
            //ViewData["PrimaryClassId"] = new SelectList(_unitOfWork.PrimaryClasses, "PrimaryClassId", "Name", student.PrimaryClassId);
            return View(student);
        }

        // POST: Student/Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Model.Models.Student student)
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
            //ViewData["PrimaryClassId"] = new SelectList(_unitOfWork.PrimaryClasses, "PrimaryClassId", "Name", student.PrimaryClassId);
            return View(student);
        }

    }
}
