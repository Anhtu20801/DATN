using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLSV.Data;
using QLSV.Data.Infrastructure;
using QLSV.Model.Models;

namespace QLSV.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClassroomsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClassroomsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/Classrooms
        public IActionResult Index()
        {
            var classrooms = _unitOfWork.ClassroomRepos.GetAll();
            if (classrooms != null)
                return View(classrooms);

            return View();
        }

        // GET: Admin/Classrooms/Details/5
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classroom = _unitOfWork.ClassroomRepos.GetSingleById(id);
            if (classroom == null)
            {
                return NotFound();
            }

            return View(classroom);
        }

        // GET: Admin/Classrooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Classrooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Classroom classroom)
        {
            ViewBag.Message = ModelState.IsValid.ToString();
            foreach (var modelStateKey in ModelState.Keys)
            {
                var modelStateVal = ModelState[modelStateKey];
                foreach (var error in modelStateVal.Errors)
                {
                    var key = modelStateKey;
                    ViewBag.Message += " | " + error.ErrorMessage;
                }
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.ClassroomRepos.Add(classroom);
                _unitOfWork.SaveChange();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Admin/Classrooms/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classroom = _unitOfWork.ClassroomRepos.GetSingleById(id);
            if (classroom == null)
            {
                return NotFound();
            }
            return View(classroom);
        }

        // POST: Admin/Classrooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Classroom classroom)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.ClassroomRepos.Update(classroom);
                _unitOfWork.SaveChange();

                return RedirectToAction("Index");
            }
            return View(classroom);
        }

        // GET: Admin/Classrooms/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classroom = _unitOfWork.ClassroomRepos.GetSingleById(id);
            if (classroom == null)
            {
                return NotFound();
            }

            return View(classroom);
        }

        // POST: Admin/Classrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var classroom = _unitOfWork.ClassroomRepos.GetSingleById(id);
            if (classroom != null)
            {
                _unitOfWork.ClassroomRepos.Delete(classroom);
            }

            _unitOfWork.SaveChange();
            return RedirectToAction(nameof(Index));
        }
    }
}
