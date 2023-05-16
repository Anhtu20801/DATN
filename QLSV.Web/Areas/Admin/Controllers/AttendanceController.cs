using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLSV.Common;
using Emgu.CV.Face;

namespace QLSV.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RolesHelper.Role_Admin)]
    public class AttendanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TrainingFace()
        {
            LBPHFaceRecognizer _recognizer = new LBPHFaceRecognizer();
            try
            {
                List<Mat> trainingFaceImages = new List<Mat>();
                List<int> labels = new List<int>();
                // Load training images from folder
                string trainingImagesFolder = @"Data/TrainingImages";
                foreach (string personFolder in Directory.GetDirectories(trainingImagesFolder))
                {
                    string personName = new DirectoryInfo(personFolder).Name;
                    foreach (string imagePath in Directory.GetFiles(personFolder))
                    {

                        Image<Gray, byte> img = new Image<Gray, byte>(imagePath);
                        trainingFaceImages.Add(img.Mat);
                        int label;
                        if (int.TryParse(personName, out label))
                            labels.Add(label);
                    }
                }
                if (trainingFaceImages.Count > 0 && labels.Count > 0)
                {
                    string path = "Data/TrainingModel/trainModel.yml";
                    _recognizer.Train(
                                new VectorOfMat(trainingFaceImages.ToArray()),
                                new VectorOfInt(labels.ToArray())
                             );

                    _recognizer.Write(path);
                    return Json(new { result = true });
                }
                else
                {
                    return Json(new { result = false });
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = false, error = ex.Message });
            }
        }

    }
}
