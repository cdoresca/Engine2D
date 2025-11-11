using System.Drawing;

namespace _2D_engine.Illumination
{
    internal class Couleur
    {
        public Color color { get; set; }
        double a, r, g, b;
        public Couleur() 
        { 
            color = Color.Black;

            r = color.R / 255.0; 
            g = color.G / 255.0; 
            b = color.B / 255.0; 
            a = color.A / 255.0;
        
        }

        public Couleur(Color color) { 
            this.color = color;
            r = color.R / 255.0;
            g = color.G / 255.0;
            b = color.B / 255.0;
            a = color.A / 255.0;

        }

        public Couleur(int a, int r, int b, int g)
        {
            color = Color.FromArgb(a, r, g, b);
            this.r = color.R / 255.0;
            this.g = color.G / 255.0;
            this.b = color.B / 255.0;
            this.a = color.A / 255.0;

        }



        public static Couleur operator *(Couleur left, double intensite)
        {
            int r = Math.Clamp((int)(left.r * Math.Abs(intensite) * 255.0), 0, 255);
            int g = Math.Clamp((int)(left.g * Math.Abs(intensite) * 255.0), 0, 255);
            int b = Math.Clamp((int)(left.b * Math.Abs(intensite) * 255.0), 0, 255);
            int a = Math.Clamp((int)(left.a * Math.Abs(intensite) * 255.0), 0, 255);

            return new Couleur(Color.FromArgb(a, r, g, b));
        }


        public static Couleur operator *(double intensite, Couleur left)
        {
            int r = Math.Clamp((int)(left.r * Math.Abs(intensite) * 255.0), 0, 255);
            int g = Math.Clamp((int)(left.g * Math.Abs(intensite) * 255.0), 0, 255);
            int b = Math.Clamp((int)(left.b * Math.Abs(intensite) * 255.0), 0, 255);
            int a = Math.Clamp((int)(left.a * Math.Abs(intensite) * 255.0), 0, 255);

            return new Couleur(Color.FromArgb(a, r, g, b));
        }

        public static Couleur operator *(Couleur right, Couleur left)
        {
            int r = Math.Clamp((int)(left.r * right.r * 255.0), 0, 255);
            int g = Math.Clamp((int)(left.g * right.g * 255.0), 0, 255);
            int b = Math.Clamp((int)(left.b * right.b * 255.0), 0, 255);
            int a = Math.Clamp((int)(left.a * right.a * 255.0), 0, 255);

            return new Couleur(Color.FromArgb(a, r, g, b));

        }

        public static Couleur operator +(Couleur right, Couleur left)
        {
            int r = Math.Clamp((int)((left.r + right.r) * 255.0), 0, 255);
            int g = Math.Clamp((int)((left.g + right.g) * 255.0), 0, 255);
            int b = Math.Clamp((int)((left.b + right.b) * 255.0), 0, 255);
            int a = Math.Clamp((int)((left.a + right.a) * 255.0), 0, 255);

            return new Couleur(Color.FromArgb(a, r, g, b));

        }


    }
}
