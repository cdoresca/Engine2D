using System.Drawing;

namespace _2D_engine
{
    internal class Image
    {
        Bitmap img;
        public Image(Bitmap img)
        {
            this.img = img;
        }
        public void saveImage(string name)
        {
            string projectDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

            // Create Images folder inside project
            string imagesFolder = Path.Combine(projectDir, "Output");


            // Save file
            string filePath = Path.Combine(imagesFolder, name);
            img.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

            Console.WriteLine("Image saved to: " + filePath);
        }
    }
}
