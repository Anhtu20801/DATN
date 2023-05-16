using Emgu.CV.Structure;
using Emgu.CV;
using Microsoft.AspNetCore.Mvc;
using QLSV.Web.Common;
using Emgu.CV.CvEnum;
using Microsoft.AspNetCore.Authorization;
using QLSV.Common;

namespace QLSV.Web.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = RolesHelper.Role_Student)]
    public class FaceDetectionController : Controller
    {
        private static string pathHaarCascade = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot\\Cascades",
            "haarcascade_frontalface_default.xml");
        private CascadeClassifier faceDetector = new CascadeClassifier(pathHaarCascade);
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetFace(string imageString, int index)
        {
            try
            {
                var imageBytes = Convert.FromBase64String(imageString.Split(',')[1]);
                var image = CommonHelper.converBytetoImage(imageBytes);

                var grayImage = image.Convert<Gray, byte>();

                //Detect face
                var faces = faceDetector.DetectMultiScale(grayImage, 1.3, 5);
                if (faces.Length <= 0)
                {
                    return Json(new { result = false, message = "Khong tim thay khuon mat"});
                }
                
                
                var cropImage = grayImage.Copy(faces[0]);
                //Normalize the image to a specific size
                Image<Gray, byte> imageNormalized = cropImage.Resize(100, 100, Inter.Cubic);
                //Apply histogram equalization to enhance contrast
                imageNormalized._EqualizeHist();
                //normalized._SmoothGaussian(3);
                imageNormalized._SmoothGaussian(3);
                //save image
                string imgFolder = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\TrainingImages\\{User.Identity.Name}");
                if (!Directory.Exists(imgFolder))
                {
                    Directory.CreateDirectory(imgFolder);
                }
                string imgPath = Path.Combine(imgFolder, User.Identity.Name + "_sv_" + index.ToString() + ".jpg");
                CvInvoke.Imwrite(imgPath, imageNormalized);

                return Json(new
                {
                    result = true,
                    faceX = faces[0].X,
                    faceY = faces[0].Y,
                    faceW = faces[0].Width,
                    faceH = faces[0].Height,
                    i = index
                });

            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message });
            }
        }
    }
}
