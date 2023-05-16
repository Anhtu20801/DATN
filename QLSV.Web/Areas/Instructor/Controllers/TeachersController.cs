using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLSV.Common;
using QLSV.Data.Infrastructure;
using QLSV.Model.Models;

namespace QLSV.Web.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    [Authorize(RolesHelper.Role_Teacher)]
    public class TeachersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeachersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/Teachers/Details/5
        public async Task<IActionResult> Details()
        {
            if (_unitOfWork.StudentRepos == null)
            {
                return NotFound();
            }

            var teacher = _unitOfWork.TeacherRepos.getByTeacherCode(User.Identity.Name);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Admin/Teachers/Edit/5
        public IActionResult Edit(int id)
        {
            ViewData["DepartmentId"] = new SelectList(_unitOfWork.DepartmentRepos.GetAll(), "DepartmentId", "Name");
            
            var teacher = _unitOfWork.TeacherRepos.GetSingleById(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Admin/Teachers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Teacher teacher)
        {
            ViewData["DepartmentId"] = new SelectList(_unitOfWork.DepartmentRepos.GetAll(), "DepartmentId", "Name");
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
    }
}
