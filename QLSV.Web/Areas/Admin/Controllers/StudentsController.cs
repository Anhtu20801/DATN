using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLSV.Common;
using QLSV.Data.Infrastructure;
using QLSV.Model.Models;
using QLSV.Web.Common;
using System.Globalization;

namespace QLSV.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RolesHelper.Role_Admin)]
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
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Model.Models.Student student)
        {
            ViewBag.ClassPrimaryList = _unitOfWork.PrimaryClassRepos.GetAll().Select(clp => new SelectListItem
            {
                Text = clp.Name,
                Value = clp.PrimaryClassId.ToString()
            });
            if (ModelState.IsValid)
            {
                try
                {
                    if (_unitOfWork.StudentRepos.getByStudentCode(student.StudentCode) != null)
                    {
                        ViewBag.Message = "Mã sinh viên đã tồn tại. Vui lòng nhập mã sinh viên mới";
                        return View(student);
                    }

                    var f = Request.Form.Files["Avatar"];
                    if (f != null)
                    {
                        string pathImg = CommonHelper.uploadFile(f, student.StudentCode);
                        if (pathImg != null)
                        {
                            var faceDetect = CommonHelper.DetectFaces(pathImg, student.StudentCode);
                            student.Avatar = faceDetect;
                        }
                    }
                    User user = new User
                    {
                        UserName = student.StudentCode
                    };

                    var result = await _unitOfWork.UserRepos.Add(user);

                    if (result.Succeeded)
                    {
                        _unitOfWork.StudentRepos.Add(student);
                        _unitOfWork.SaveChange();

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Message = "Khong tao duoc user";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
            }

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
            ViewBag.OldAvatar = Convert.ToBase64String(student.Avatar);
            return View(student);
        }

        // POST: Admin/Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Model.Models.Student student)
        {
            ViewBag.ClassPrimaryList = _unitOfWork.PrimaryClassRepos.GetAll().Select(clp => new SelectListItem
            {
                Text = clp.Name,
                Value = clp.PrimaryClassId.ToString()
            });

            if (id != student.StudentId)
            {
                return NotFound();
            }
           
            if (ModelState.IsValid)
            {
                try
                {
                    var f = Request.Form.Files["Avatar"];
                    var oldAvatar = Request.Form["OldAvatar"];
                    if (f != null)
                    {
                        string pathImg = CommonHelper.uploadFile(f, student.StudentCode);
                        if (pathImg != null)
                        {
                            var faceDetect = CommonHelper.DetectFaces(pathImg, student.StudentCode);
                            if (faceDetect != null)
                            {
                                student.Avatar = faceDetect;
                            }
                            else
                            {
                                student.Avatar = Convert.FromBase64String(oldAvatar);
                            }    
                        }
                    }
                    else
                    {
                        student.Avatar = Convert.FromBase64String(oldAvatar);
                    }
                    _unitOfWork.StudentRepos.Update(student);
                    _unitOfWork.SaveChange();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

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
                string imgFolder = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\TrainingImages\\{student.StudentCode}");
                Directory.Delete(imgFolder, true);
     
                _unitOfWork.ResultRepos.DeleteMulti(r => r.StudentId == student.StudentId);
                _unitOfWork.UserRepos.Delete(student.StudentCode);
                _unitOfWork.StudentRepos.Delete(student);
            }

            _unitOfWork.SaveChange();
            return RedirectToAction(nameof(Index));
        }


    }
}
