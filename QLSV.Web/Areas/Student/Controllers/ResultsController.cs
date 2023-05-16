using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLSV.Common;
using QLSV.Data.Infrastructure;
using System.Data;

namespace QLSV.Web.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = RolesHelper.Role_Student)]
    public class ResultsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResultsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Student/Results
        public async Task<IActionResult> Index()
        {
            try
            {
                var student = _unitOfWork.StudentRepos.getByStudentCode(User.Identity.Name);
                var results = _unitOfWork.ResultRepos.GetAll(r => r.StudentId == student.StudentId);
                if (results == null)
                {
                    return View();
                }

                ViewBag.Student = student;
                return View(results);

            }catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }

        }
    }
}
