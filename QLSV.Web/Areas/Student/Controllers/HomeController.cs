using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLSV.Common;

namespace QLSV.Web.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = RolesHelper.Role_Student)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
