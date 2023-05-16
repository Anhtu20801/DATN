using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLSV.Common;
using QLSV.Data.Infrastructure;
using QLSV.Model.Models;

namespace QLSV.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RolesHelper.Role_Admin)]
    public class DepartmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/Departments
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepos.GetAll();
            if (departments != null)
                return View(departments);

            return View();
        }

        // GET: Admin/Departments/Details/5
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _unitOfWork.DepartmentRepos.GetSingleById(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Admin/Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepos.Add(department);
                _unitOfWork.SaveChange();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Admin/Departments/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _unitOfWork.DepartmentRepos.GetSingleById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Admin/Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepos.Update(department);
                _unitOfWork.SaveChange();

                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Admin/Departments/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _unitOfWork.DepartmentRepos.GetSingleById(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Admin/Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var department = _unitOfWork.DepartmentRepos.GetSingleById(id);
            if (department != null)
            {
                _unitOfWork.DepartmentRepos.Delete(department);
            }

            _unitOfWork.SaveChange();
            return RedirectToAction(nameof(Index));
        }
    }
}
