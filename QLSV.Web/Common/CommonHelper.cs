using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.Face;
using System.Drawing;
using Emgu.CV.Util;
using QLSV.Model.Models;

namespace QLSV.Web.Common
{
    public static class CommonHelper
    {
        public static LBPHFaceRecognizer _recognizer = new LBPHFaceRecognizer();
        public static Image<Bgr, byte> converBytetoImage(byte[] bytes)
        {
            var stream = new MemoryStream(bytes);
            var bitmapImage = new Bitmap(stream);
            stream.Close();
            var image = bitmapImage.ToImage<Bgr, byte>();
            return image;
        }
        public static string uploadFile(IFormFile f,string path, string newFileName = "")
        {

            string fileExtention = Path.GetExtension(f.FileName);
            string fileName = newFileName + fileExtention;
            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);
            var stream = new FileStream(uploadpath, FileMode.Create);
            f.CopyToAsync(stream);
            stream.Close();
            return uploadpath;
        }

        public static Rectangle[] DetectFaces(byte[] img)
        {
            var image = converBytetoImage(img);

            // Load the face detection classifier
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Cascades", "haarcascade_frontalface_alt.xml");
            var haarCascade = new CascadeClassifier(path);

            //Convert Brg image to gray image
            using var gray = new Mat();
            CvInvoke.CvtColor(image, gray, ColorConversion.Bgr2Gray);
            CvInvoke.EqualizeHist(gray, gray);
            
            //Detect face in image
            var faces = haarCascade.DetectMultiScale(gray, 1.3, 4);
            return faces;
            
        }
        public static byte[] CroppedFaceImage(Rectangle[] faces, byte[] img)
        {
            var image = converBytetoImage(img);
            if (faces.Length > 0)
            {
                foreach (var face in faces)
                {

                    //crop image
                    var px = image.Width/2 - face.Width/2;
                    var py = image.Height/2 - face.Height/2;
                    Rectangle rect = new Rectangle(px, py, 248, 340);
                    var croppedImage = image.GetSubRect(rect);
                    croppedImage.Resize(120, 120, Inter.Cubic);
                    //save image
                    //string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\", fileName);
                    //croppedImage.Save(imgPath);

                    byte[] croppedImageByte = croppedImage.ToJpegData();
                    return croppedImageByte;
                }

            }
            return null;
        }
        public static string RecognizeFace(Image<Bgr, byte> image)
        {
            _recognizer.Read("Data/TrainingModel/trainModel.yml");
            var grayImage = image.Convert<Gray, byte>();
            //Detect face
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Cascades", "haarcascade_frontalface_alt.xml");
            var faceDetector = new CascadeClassifier(path); 
            var faces = faceDetector.DetectMultiScale(grayImage, 1.3, 5);

            foreach (var face in faces)
            {
                var cropImage = grayImage.GetSubRect(face);
                var resizedImage = cropImage.Resize(100, 100, Inter.Cubic);

                var result = _recognizer.Predict(resizedImage);

                if (result.Label != -1 && result.Distance < 100)
                {
                    return result.Label.ToString();
                }
            }

            return null;
        }

        public static bool TrainingFace(List<Student> data)
        {
            try
            {
                List<Mat> trainingFaceImages = new List<Mat>();
                List<int> labels = new List<int>();
                foreach (var face in data)
                {
                    if (face.Avatar != null)
                    {
                        Image<Bgr, byte> image = converBytetoImage(face.Avatar);
                        trainingFaceImages.Add(image.Mat);
                        labels.Add(face.StudentId);
                    }
                }

                _recognizer.Train(
                    new VectorOfMat(trainingFaceImages.ToArray()),
                    new VectorOfInt(labels.ToArray())
                 );
                _recognizer.Write("Data/TrainingModel/trainModel.xml");
                return true;
            }catch(Exception ex)
            {
                var message = ex.Message;
                return false;
            }
            
            
        }

    }
}
