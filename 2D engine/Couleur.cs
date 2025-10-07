using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _2D_engine
{
    internal class Couleur
    {
        public Color color { get; set; }
        public Couleur() { color = Color.Black; }

        public Couleur(Color color) { this.color = color; }

        public static Color operator *(Couleur left,double a)
        {
            int r = Math.Clamp((int)(left.color.R * Math.Abs(a)), 0, 255);
            int g = Math.Clamp((int)(left.color.G * Math.Abs(a)), 0, 255);
            int b = Math.Clamp((int)(left.color.B * Math.Abs(a)), 0, 255);

            return Color.FromArgb(left.color.A, r, g, b);
        }


        public static Color operator *(double a, Couleur left)
        {
            int r = Math.Clamp((int)(left.color.R * a), 0, 255);
            int g = Math.Clamp((int)(left.color.G * a), 0, 255);
            int b = Math.Clamp((int)(left.color.B * a), 0, 255);

            return Color.FromArgb(left.color.A, r, g, b);
        }


    }
}
