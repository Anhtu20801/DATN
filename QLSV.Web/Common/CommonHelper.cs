using Emgu.CV.Structure;
using Emgu.CV;
using System.Drawing;
using Emgu.CV.CvEnum;

namespace QLSV.Web.Common
{
    public static class CommonHelper
    {
        public static Image<Bgr, byte> converBytetoImage(byte[] bytes)
        {
            var stream = new MemoryStream(bytes);
            stream.Position = 0;
            var bitmapImage = new Bitmap(stream);
            stream.Close();
            var image = bitmapImage.ToImage<Bgr, byte>();
            return image;
        }
        
        public static string uploadFile(IFormFile f, string newFileName = "")
        {

            string fileExtention = Path.GetExtension(f.FileName);
            string file = newFileName + fileExtention;
            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", file);
            var stream = new FileStream(uploadpath, FileMode.Create);
            f.CopyTo(stream);
            stream.Close();
            return uploadpath;
        }

       
        public static byte[]? DetectFaces(string pathImage, string fileName)
        {
            Image<Bgr, byte> image = new Image<Bgr, byte>(pathImage);

            // Load the face detection classifier
            string pathClassifier = Path.Combine(Directory.GetCurrentDirectory(),
                                    "wwwroot\\Cascades",
                                    "haarcascade_frontalface_default.xml");
            using var haarCascade = new CascadeClassifier(pathClassifier);

            //Convert Brg image to gray image
            using var gray = new Mat();
            CvInvoke.CvtColor(image, gray, ColorConversion.Bgr2Gray);
            CvInvoke.EqualizeHist(gray, gray);

            //Detect face in image
            var faces = haarCascade.DetectMultiScale(gray, 1.3, 10);
            if (faces.Length > 0)
            {
                foreach (var face in faces)
                {
                    //crop image
                    Rectangle rect = new Rectangle(face.X - 10, face.Y - 40, face.Width + 30, face.Height + 90);
                    var croppedImage = (image.GetSubRect(rect)).Resize(120, 120, Inter.Cubic);

                    //save image
                    string imgFolder = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\TrainingImages\\{fileName}");
                    if (!Directory.Exists(imgFolder))
                    {
                        Directory.CreateDirectory(imgFolder);
                    }
                    for (int i = 0; i < 20; i++)
                    {
                        string imgPath = Path.Combine(imgFolder, fileName + "_" + i + ".jpg");
                        CvInvoke.Imwrite(imgPath, croppedImage.Convert<Gray, byte>());

                    }

                    byte[] croppedImageByte = croppedImage.ToJpegData();
                    return croppedImageByte;
                }

            }
            return null;
        }

        public static TimeSpan timeLog(DateTime startTime)
        {
            return DateTime.Now - startTime;
        }
    }
}
