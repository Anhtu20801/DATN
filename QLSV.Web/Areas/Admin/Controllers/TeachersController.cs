using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLSV.Common;
using QLSV.Data.Infrastructure;
using QLSV.Model.Models;

namespace QLSV.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RolesHelper.Role_Admin)]
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
            var teachers = _unitOfWork.TeacherRepos.GetAll();
            if (teachers != null)
                return View(teachers);

            return View();
        }

        // GET: Admin/Teachers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (_unitOfWork.StudentRepos == null)
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

        // GET: Admin/Teachers/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_unitOfWork.DepartmentRepos.GetAll(), "DepartmentId", "Name");
            return View();
        }

        // POST: Admin/Teachers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            ViewData["DepartmentId"] = new SelectList(_unitOfWork.DepartmentRepos.GetAll(), "DepartmentId", "Name");
            if (ModelState.IsValid)
            {
                if (_unitOfWork.TeacherRepos.getByTeacherCode(teacher.TeacherCode) != null)
                {
                    ViewBag.Message = "Mã giáo viên đã tồn tại. Vui lòng nhập mã mới";
                    return View(teacher);
                }
                User user = new User
                {
                    UserName = teacher.TeacherCode
                };

                var result = await _unitOfWork.UserRepos.Add(user, role: RolesHelper.Role_Teacher);

                if (result.Succeeded)
                {
                    _unitOfWork.TeacherRepos.Add(teacher);
                    _unitOfWork.SaveChange();
                    return RedirectToAction(nameof(Index));
                }
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
            ViewData["DepartmentId"] = new SelectList(_unitOfWork.DepartmentRepos.GetAll(), "DepartmentId", "Name");

            return View(teacher);
        }

        // POST: Admin/Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            ViewData["DepartmentId"] = new SelectList(_unitOfWork.DepartmentRepos.GetAll(), "DepartmentId", "Name");

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
