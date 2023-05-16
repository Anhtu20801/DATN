using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLSV.Common;
using QLSV.Data;
using QLSV.Data.Infrastructure;
using QLSV.Model.Models;
using System.Data;

namespace QLSV.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RolesHelper.Role_Admin)]
    public class MajorsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MajorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/Majors
        public async Task<IActionResult> Index()
        {
            var majors = _unitOfWork.MajorRepos.GetAll();
            return View(majors);
        }

        // GET: Admin/Majors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var major = _unitOfWork.MajorRepos.GetSingleById(id);
            if (major == null)
            {
                return NotFound();
            }

            return View(major);
        }

        // GET: Admin/Majors/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_unitOfWork.DepartmentRepos.GetAll(), "DepartmentId", "Name");
            return View();
        }

        // POST: Admin/Majors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Major major)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.MajorRepos.Add(major);
                await _unitOfWork.SaveChangeAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_unitOfWork.DepartmentRepos.GetAll(), "DepartmentId", "Name");
            return View(major);
        }

        // GET: Admin/Majors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["DepartmentId"] = new SelectList(_unitOfWork.DepartmentRepos.GetAll(), "DepartmentId", "Name");

            var major = _unitOfWork.MajorRepos.GetSingleById(id);
            if (major == null)
            {
                return NotFound();
            }

            return View(major);
        }

        // POST: Admin/Majors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Major major)
        {
            ViewData["DepartmentId"] = new SelectList(_unitOfWork.DepartmentRepos.GetAll(), "DepartmentId", "Name");
            if (id != major.MajorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.MajorRepos.Update(major);
                    await _unitOfWork.SaveChangeAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            return View(major);
        }

        // GET: Admin/Majors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var major = _unitOfWork.MajorRepos.GetSingleById(id);
            if (major == null)
            {
                return NotFound();
            }

            return View(major);
        }

        // POST: Admin/Majors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var major = _unitOfWork.MajorRepos.GetSingleById(id);
            if (major != null)
            {
                _unitOfWork.MajorRepos.Delete(major);
            }

            await _unitOfWork.SaveChangeAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
