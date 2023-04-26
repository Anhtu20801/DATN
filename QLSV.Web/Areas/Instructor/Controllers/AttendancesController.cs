using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV;
using Microsoft.AspNetCore.Mvc;
using QLSV.Data.Infrastructure;
using QLSV.Web.Common;
using Emgu.CV.Face;
using Emgu.CV.CvEnum;

namespace QLSV.Web.Areas.Instructor.Controllers
{

    [Area("Instructor")]
    public class AttendancesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private LBPHFaceRecognizer _recognizer = new LBPHFaceRecognizer();
        public AttendancesController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var attendances = _unitOfWork.AttendanceRepos.GetAll();

            if (attendances != null)
                return View(attendances);

            return View();
        }

        public IActionResult TrainingFace()
        {
            try
            {
                List<Mat> trainingFaceImages = new List<Mat>();
                List<int> labels = new List<int>();
                // Load training images from folder
                string trainingImagesFolder = @"Data/Image_training";
                var index = 1;
                foreach (string imagePath in Directory.GetFiles(trainingImagesFolder))
                {

                    Image<Gray, byte> img = new Image<Gray, byte>(imagePath);
                    trainingFaceImages.Add(img.Mat);
                    labels.Add(index);
                    index++;
                }

                LBPHFaceRecognizer _recognizer = new LBPHFaceRecognizer();
                _recognizer.Train(
                        new VectorOfMat(trainingFaceImages.ToArray()),
                        new VectorOfInt(labels.ToArray())
                     );
                _recognizer.Write("Data/trainModel.yml");
                return Json(new { result = true, labelList = labels });
            }
            catch (Exception ex)
            {
                return Json(new { result = false, error = ex.Message });
            }

        }
        public IActionResult Recognize()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Recognize(string imageString)
        {
            try
            {
                var imageBytes = Convert.FromBase64String(imageString.Split(',')[1]);
                var img = CommonHelper.converBytetoImage(imageBytes);

                _recognizer.Read("Data/TrainingModel/trainModel.yml");
                var grayImage = img.Convert<Gray, byte>();
                //Detect face
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Cascades", "haarcascade_frontalface_default.xml");
                var faceDetector = new CascadeClassifier(path);
                var faces = faceDetector.DetectMultiScale(grayImage, 1.3, 5);

                foreach (var face in faces)
                {
                    var cropImage = grayImage.GetSubRect(face);
                    var resizedImage = cropImage.Resize(100, 100, Inter.Cubic);

                    var result = _recognizer.Predict(resizedImage);

                    if (result.Label != -1 && result.Distance < 100)
                    {
                        bool check = false;
                        check = true;
                        //var student = _unitOfWork.StudentRepos.GetSingleById(labelFace);
                        DateTime attenTime = DateTime.Now;

                        return Json(new { result = check, label = result.Label, time = attenTime });
                    }
                }
                return Json(new { result = false});
            }
            catch (Exception ex)
            {
                return Json(new { result = false, error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DetectFace(string imageString)
        {
            var imageBytes = Convert.FromBase64String(imageString.Split(',')[1]);
            var faces = CommonHelper.DetectFaces(imageBytes);
            if (faces.Length > 0)
            {
                foreach (var face in faces)
                {
                    return Json(new { result = true, faceWidth = face.Width, faceHeight = face.Height });

                }

            }
            return Json(new { result = false, image = imageBytes });
        }
    }
}
