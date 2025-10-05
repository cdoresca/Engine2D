using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _2D_engine.Algebre
{
    internal class Vecteur
    {
        double x;
        double y;
        double z;

        public double norme;

        public Vecteur(double x = 0, double y = 0, double z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            norme = norm();

        }

        public Vecteur(Vecteur v)
        {

            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
            norme = norm();
        }

        private double norm()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        public Vecteur normalization()
        {
            return this / norme;
        }

        public double dot(Vecteur vector)
        {
            return vector.x * x + vector.y * y + vector.z * z;
        }

        public Normal cross(Vecteur v)
        {
            return new Normal(y * v.z - z * v.y, x * v.z - z * v.x, x * v.y - y * v.x);
        }

        public double sin() { return y / norme; }
        public double cos() { return x / norme; }
        public double tan() { return y / x; }
        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }

        public static Vecteur operator +(Vecteur a, Vecteur b)
        {
            return new Vecteur(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vecteur operator -(Vecteur a, Vecteur b)
        {
            return new Vecteur(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vecteur operator *(double a, Vecteur b)
        {
            return new Vecteur(a * b.x, a * b.y, a * b.z);
        }

        public static Vecteur operator *(Vecteur b, double a)
        {
            return new Vecteur(a * b.x, a * b.y, a * b.z);
        }

        public static double operator *(Vecteur a, Vecteur b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static Vecteur operator /(Vecteur b, double a)
        {
            return new Vecteur(b.x / a, b.y / a, b.z / a);
        }

        public static Normal operator %(Vecteur a, Vecteur b)
        {
            return a.cross(b);
        }

        public double this[int index]
        {
            get
            {
                return index switch
                {
                    0 => x,
                    1 => y,
                    2 => z,
                    _ => throw new IndexOutOfRangeException("Index doit être entre 0 et 3.")
                };
            }
            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    default: throw new IndexOutOfRangeException("Index doit être entre 0 et 3.");
                }
            }
        }
    }
}
