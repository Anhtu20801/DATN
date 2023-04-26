using Emgu.CV.Structure;
using Emgu.CV;
using Microsoft.AspNetCore.Mvc;

namespace QLSV.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FaceDetection()
        {
            Image<Bgr, byte> img = new Image<Bgr, byte>(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/custom/images/Nguyen Anh Tu.jpg"));

            CascadeClassifier haarCascade = new CascadeClassifier(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/haarcascade_frontalface_alt.xml"));

            var faces = haarCascade.DetectMultiScale(img);

            foreach (var face in faces)
            {
                img.Draw(face, new Bgr(0, 0, 255), 2);
            }

            return View(img);
        }

    }
}
