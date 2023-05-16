using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLSV.Common;
using QLSV.Data.Infrastructure;
using QLSV.Model.Models;

namespace QLSV.Web.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = RolesHelper.Role_Student)]
    public class RegisterClassroomController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterClassroomController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var classes = _unitOfWork.ClassroomRepos.GetAll();
            
            if (classes != null)
                return View(classes);
            
            return View();
        }
        public IActionResult InforRegisteredClass()
        {
            var student = _unitOfWork.StudentRepos.getByStudentCode(User.Identity.Name);
            if (student != null)
            {
                var result = _unitOfWork.ResultRepos.GetAll(r => r.StudentId == student.StudentId);

                if(result != null)
                {
                    return View(result);
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult Register(int id)
        {
            var classroom = _unitOfWork.ClassroomRepos.GetSingleById(id);
            if(classroom!= null)
            {
                var student = _unitOfWork.StudentRepos.getByStudentCode(User.Identity.Name);
                var countStudent = classroom.CountStudent;
                
                if(countStudent >= classroom.MaxStudent) 
                {
                    return Json(new {message = "Đăng ký không thành công. Lớp đã đầy"});
                }
                
                var checkStudent = _unitOfWork.ResultRepos.GetAll(r => r.ClassroomId == classroom.ClassroomId && r.StudentId == student.StudentId).FirstOrDefault();
                if(checkStudent!= null) 
                {
                    return Json(new {message = "Bạn đã đăng ký môn học này!" });
                }

                Result result = new Result()
                {
                    ClassroomId = classroom.ClassroomId,
                    StudentId = student.StudentId,
                    RegisterDate = DateTime.Now,
                    Status = true
                };

                classroom.CountStudent = countStudent + 1;
                _unitOfWork.ClassroomRepos.Update(classroom);
                _unitOfWork.ResultRepos.Add(result);
                _unitOfWork.SaveChange();
                return Json(new { result = true, classname = classroom.Course.Name, message = "Đăng ký thành công" });
            }
            return Json(new { message = "Đăng ký không thành công" });
        }

        
        [HttpPost]
        public IActionResult Cancel(int classId, int studentId)
        {
            try
            {
                var r = _unitOfWork.ResultRepos.GetAll().FirstOrDefault(r => (r.ClassroomId == classId && r.StudentId == studentId));
                if (r != null)
                {
                    _unitOfWork.ResultRepos.Delete(r);
                    _unitOfWork.SaveChange();
                    return Json(new { result = "Hủy thành công"});
                }
                return Json(new { result = false, classid = classId, studentid = studentId });
            }catch(Exception ex)
            {
                return Json(new { error = ex.Message});
            }
        }
    }
}
