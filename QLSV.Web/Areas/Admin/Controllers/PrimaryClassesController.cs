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
    public class PrimaryClassesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PrimaryClassesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/PrimaryClasses
        public async Task<IActionResult> Index()
        {
            var primaryClasses = _unitOfWork.PrimaryClassRepos.GetAll();
            return View(primaryClasses);
        }

        // GET: Admin/PrimaryClasses/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var primaryClass = _unitOfWork.PrimaryClassRepos.GetSingleById(id);
            if (primaryClass == null)
            {
                return NotFound();
            }

            return View(primaryClass);
        }

        // GET: Admin/PrimaryClasses/Create
        public IActionResult Create()
        {
            ViewData["MajorId"] = new SelectList(_unitOfWork.MajorRepos.GetAll(), "MajorId", "Name");
            return View();
        }

        // POST: Admin/PrimaryClasses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrimaryClassId,Name,Description,MajorId")] PrimaryClass primaryClass)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.PrimaryClassRepos.Add(primaryClass);
                await _unitOfWork.SaveChangeAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MajorId"] = new SelectList(_unitOfWork.MajorRepos.GetAll(), "MajorId", "Name");
            return View(primaryClass);
        }

        // GET: Admin/PrimaryClasses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["MajorId"] = new SelectList(_unitOfWork.MajorRepos.GetAll(), "MajorId", "Name");

            var primaryClass = _unitOfWork.PrimaryClassRepos.GetSingleById(id);
            if (primaryClass == null)
            {
                return NotFound();
            }

            return View(primaryClass);
        }

        // POST: Admin/PrimaryClasses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrimaryClassId,Name,Description,MajorId")] PrimaryClass primaryClass)
        {
            ViewData["MajorId"] = new SelectList(_unitOfWork.MajorRepos.GetAll(), "MajorId", "Name");
            if (id != primaryClass.PrimaryClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.PrimaryClassRepos.Update(primaryClass);
                    await _unitOfWork.SaveChangeAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(primaryClass);
        }

        // GET: Admin/PrimaryClasses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var primaryClass = _unitOfWork.PrimaryClassRepos.GetSingleById(id);
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

            var primaryClass = _unitOfWork.PrimaryClassRepos.GetSingleById(id);
            if (primaryClass != null)
            {
                _unitOfWork.PrimaryClassRepos.Delete(primaryClass);
            }
            
            await _unitOfWork.SaveChangeAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
