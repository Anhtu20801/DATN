using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV;
using Microsoft.AspNetCore.Mvc;
using QLSV.Data.Infrastructure;
using QLSV.Web.Common;
using Emgu.CV.Face;
using Emgu.CV.CvEnum;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using QLSV.Common;
using QLSV.Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace QLSV.Web.Areas.Instructor.Controllers
{

    [Area("Instructor")]
    [Authorize(Roles = RolesHelper.Role_Teacher)]
    public class AttendancesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private static string pathHaarCascade = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot\\Cascades",
            "haarcascade_frontalface_default.xml");
        private CascadeClassifier faceDetector = new CascadeClassifier(pathHaarCascade);

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

        public IActionResult Recognize()
        {
            Teacher teacher = _unitOfWork.TeacherRepos.getByTeacherCode(User.Identity.Name);
            if (teacher != null)
            {
                ViewBag.ClassroomList = _unitOfWork.ClassroomRepos.GetAll(cl => cl.TeacherId == teacher.TeacherId).Select(cl => new SelectListItem
                {
                    Text = cl.Course.Name,
                    Value = cl.ClassroomId.ToString()
                });
            }
            return View();
        }

        [HttpPost]
        public IActionResult Recognize(string imageString, int classroomId)
        {
            try
            {
                _recognizer.Read("Data/TrainingModel/trainModel.yml");
                DateTime  startTime = DateTime.Now;
                if (imageString != null)
                {
                    var imageBytes = Convert.FromBase64String(imageString.Split(',')[1]);
                    var img = CommonHelper.converBytetoImage(imageBytes);

                    var grayImage = img.Convert<Gray, byte>();

                    //Detect face
                    var faces = faceDetector.DetectMultiScale(grayImage, 1.3, 5);
                    if (faces.Length <= 0)
                    {
                        return Json(new { result = false, 
                                        message = "Khong tim thay khuon mat", 
                                        timelog = CommonHelper.timeLog(startTime) });
                    }

                    foreach (var face in faces)
                    {
                        //Rectangle rect = new Rectangle(face.X - 10, face.Y - 40, face.Width + 30, face.Height + 90);
                        var cropImage = grayImage.GetSubRect(face);
                        //Normalize the image to a specific size
                        Image<Gray, byte> normalized = cropImage.Resize(100, 100, Inter.Cubic);
                        //Apply histogram equalization to enhance contrast
                        normalized._EqualizeHist();
                        //normalized._SmoothGaussian(3);
                        normalized._SmoothGaussian(3);
                        var result = _recognizer.Predict(normalized);

                        if (result.Label != -1 && result.Distance < 105)
                        {
                            var student = _unitOfWork.StudentRepos.getByStudentCode(result.Label.ToString());
                            var studentOfCLass = _unitOfWork.ResultRepos.GetAll(r => r.StudentId == student.StudentId && r.ClassroomId == classroomId);
                            if (studentOfCLass == null)
                                return Json(new 
                                {
                                    result = true,
                                    check = 0,
                                    faceX = face.X,
                                    faceY = face.Y,
                                    faceW = face.Width,
                                    faceH = face.Height,
                                    timelog = CommonHelper.timeLog(startTime),
                                    distance = result.Distance
                                });

                            DateTime attenTime = DateTime.Now;
                            string timeDisplay = string.Format($"{attenTime.Hour}:{attenTime.Minute}\t{attenTime.Day}-{attenTime.Month}-{attenTime.Year}");

                            return Json(new
                            {
                                result = true,
                                check = 1,
                                faceX = face.X,
                                faceY = face.Y,
                                faceW = face.Width,
                                faceH = face.Height,
                                studentCode = result.Label,
                                name = student.Name,
                                time = timeDisplay,
                                timelog = CommonHelper.timeLog(startTime).Milliseconds,
                                distance = result.Distance
                            }); ;
                        }

                        return Json(new
                        {
                            result = true,
                            faceX = face.X,
                            faceY = face.Y,
                            faceW = face.Width,
                            faceH = face.Height,
                            label = result.Label,
                            timelog = CommonHelper.timeLog(startTime).Milliseconds,
                            distance = result.Distance
                        });
                    }

                }
                return Json(new {result = false, 
                                 image = imageString, 
                                 timelog = CommonHelper.timeLog(startTime).Milliseconds});
            }
            catch (Exception ex)
            {
                return Json(new { result = false, error = ex.Message});
            }
        }
    }
}
