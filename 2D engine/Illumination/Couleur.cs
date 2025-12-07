using System.Drawing;

namespace _2D_engine.Illumination
{
    internal class Couleur
    {
        public Color color { get; set; }
        public double a, r, g, b;
        public Couleur()
        {
           r=g=b = 0.0;
            a=1.0;

            
        }

        public Couleur(Color color)
        {
            this.color = color;
            r = color.R / 255.0;
            g = color.G / 255.0;
            b = color.B / 255.0;
            a = color.A / 255.0;
            
        }

        public Couleur(double a, double r, double g, double b)
        {
  
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;


            
        }



        public static Couleur operator *(Couleur left, double intensite)
        {

            return new Couleur(left.a * intensite,left.r * intensite,left.g * intensite,left.b *intensite);
        }


        public static Couleur operator /(Couleur left, double intensite)
        {

            return new Couleur(left.a / intensite, left.r / intensite, left.g / intensite, left.b / intensite);
        }


        public static Couleur operator *(double intensite, Couleur left)
        {


            return new Couleur(left.a * intensite, left.r * intensite, left.g * intensite, left.b * intensite);
        }

        public static Couleur operator *(Couleur right, Couleur left)
        {
         

            return new Couleur(right.a *left.a,right.r *left.r,right.g * left.g,right.b *left.b);

        }

        public static Couleur operator +(Couleur right, Couleur left)
        {


            return new Couleur(right.a + left.a, right.r + left.r, right.g + left.g, right.b + left.b);

        }
        public void Clamp()
        {
            r = Math.Clamp(r, 0.0, 1.0);
            g = Math.Clamp(g, 0.0, 1.0);
            b = Math.Clamp(b, 0.0, 1.0);
            a = Math.Clamp(a, 0.0, 1.0);
        }

        public Color ToColor()
        {
            Clamp();
            return Color.FromArgb(
                (int)(a * 255),
                (int)(r * 255),
                (int)(g * 255),
                (int)(b * 255)
            );
        }


    }
}
