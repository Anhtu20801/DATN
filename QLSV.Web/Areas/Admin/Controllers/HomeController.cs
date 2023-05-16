using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLSV.Common;

namespace QLSV.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RolesHelper.Role_Admin)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
