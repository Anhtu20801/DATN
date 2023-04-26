using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Drawing;

namespace QLSV.Common
{
    public static class CommonHepler
    {
        public static byte[] DetectFaces(byte[] img, string fileName)
        {
            //string pathImage = Directory.GetCurrentDirectory() + "\\wwwroot\\images\\before\\" + fileName;
            var stream = new MemoryStream(img);
            var bitmapImage = new Bitmap(stream);

            var image = bitmapImage.ToImage<Bgr, byte>();
            //Image<Bgr, byte> image = new Image<Bgr, byte>(pathImage);

            // Load the face detection classifier
            using var haarCascade = new CascadeClassifier("haarcascade_frontalface_default.xml");

            //Convert Brg image to gray image
            using var gray = new Mat();
            CvInvoke.CvtColor(image, gray, ColorConversion.Bgr2Gray);
            CvInvoke.EqualizeHist(gray, gray);
            var grayImage = image.Convert<Gray, byte>();

            //Detect face in image
            var faces = haarCascade.DetectMultiScale(gray, 1.3, 4);
            if (faces.Length > 0)
            {
                foreach (var face in faces)
                {

                    //crop image
                    //Rectangle rect = new Rectangle(face.X + 4, face.Y - 25, 248, 340);
                    var croppedImage = image.GetSubRect(face);
                    //save image
                    string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\", fileName);
                    croppedImage.Save(imgPath);

                    byte[] croppedImageByte = croppedImage.ToJpegData();
                    return croppedImageByte;
                }

            }
            return null;
        }
    }
}
