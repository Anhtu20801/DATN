using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLSV.Common;
using System.Data;

namespace QLSV.Web.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    [Authorize(Roles = RolesHelper.Role_Admin + "," + RolesHelper.Role_Teacher)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
