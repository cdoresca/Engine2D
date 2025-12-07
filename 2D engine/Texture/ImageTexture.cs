using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Illumination;
using _2D_engine.Lights;
using _2D_engine.Trace;
using static System.Net.Mime.MediaTypeNames;

namespace _2D_engine.Texture
{
    internal class ImageTexture : Texture
    {
        Bitmap img;
        
        public ImageTexture(string name)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(name);
            img = new Bitmap(originalImage);
        }

        public override Couleur GetCouleur(Intersection info)
        {
          
            

            double x= info.u * (img.Width - 1);
            double y = info.v * (img.Height - 1);

            
            return Bilinear(x, y);
        }

        public void SetImage(Bitmap i) {  this.img = i; }

        public Couleur Bilinear(double x,double y)
        {
            int x1 = (int)Math.Floor(x);
            int y1 = (int)Math.Floor(y);
            int x2 = Math.Min((int)Math.Ceiling(x), img.Width - 1);
            int y2 = Math.Min((int)Math.Ceiling(y), img.Height - 1);

            Couleur c11 = new Couleur(img.GetPixel(x1, y1));
            Couleur c12 = new Couleur(img.GetPixel(x1, y2));
            Couleur c21 = new Couleur(img.GetPixel(x2, y1));
            Couleur c22 = new Couleur(img.GetPixel(x2, y2));

            double dx = x - x1;
            double dy = y - y1;

            Couleur c1 = (1 - dx) * c11 + dx * c21;
            Couleur c2 = (1 - dx) * c12 + dx * c22;

            return (1 - dy) * c1 + dy * c2;
        }



    }
}
