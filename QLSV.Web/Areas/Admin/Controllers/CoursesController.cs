using Microsoft.AspNetCore.Mvc;
using QLSV.Data.Infrastructure;
using QLSV.Model.Models;

namespace QLSV.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoursesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoursesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/Courses
        public IActionResult Index()
        {
            var courses = _unitOfWork.CourseRepos.GetAll();
            if (courses != null)
                return View(courses);

            return View();
        }

        // GET: Admin/Courses/Details/5
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _unitOfWork.CourseRepos.GetSingleById(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Admin/Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
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
                _unitOfWork.CourseRepos.Add(course);
                _unitOfWork.SaveChange();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Admin/Courses/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _unitOfWork.CourseRepos.GetSingleById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Admin/Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.CourseRepos.Update(course);
                _unitOfWork.SaveChange();

                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Admin/Courses/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _unitOfWork.CourseRepos.GetSingleById(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Admin/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var course = _unitOfWork.CourseRepos.GetSingleById(id);
            if (course != null)
            {
                _unitOfWork.CourseRepos.Delete(course);
            }

            _unitOfWork.SaveChange();
            return RedirectToAction(nameof(Index));
        }
    }
}
