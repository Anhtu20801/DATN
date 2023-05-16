using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLSV.Common;
using QLSV.Data.Infrastructure;
using QLSV.Web.Common;
using System.Data;

namespace QLSV.Web.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = RolesHelper.Role_Student)]
    public class StudentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Details()
        {
            if (_unitOfWork.StudentRepos == null)
            {
                return NotFound();
            }

            var student = _unitOfWork.StudentRepos.getByStudentCode(User.Identity.Name);
            if (student == null)
            {
                return View();
            }

            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = _unitOfWork.StudentRepos.GetSingleById(id);
            ViewBag.ClassPrimaryList = _unitOfWork.PrimaryClassRepos.GetAll().Select(clp => new SelectListItem
            {
                Text = clp.Name,
                Value = clp.PrimaryClassId.ToString()
            });
            if (student == null)
            {
                return View();
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

            if (ModelState.IsValid)
            {
                try
                {
                    var f = Request.Form.Files["AvatarImage"];
                    var oldAvatar = Request.Form["OldAvatar"];
                    if (f != null)
                    {
                        string pathImg = CommonHelper.uploadFile(f, student.StudentCode);
                        if (pathImg != null)
                        {
                            var faceDetect = CommonHelper.DetectFaces(pathImg, student.StudentCode);
                            student.Avatar = faceDetect;
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
                return RedirectToAction(nameof(Details));
            }

            return View(student);
        }
    }
}
