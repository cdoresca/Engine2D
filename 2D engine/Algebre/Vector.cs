using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _2D_engine.Algebre
{
    internal class Vector
    {
        double x;
        double y;
        double z;

        public double norme;

        public Vector(double x = 0, double y = 0, double z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
             norme = norm();

        }

        public Vector( Vector v)
        {
           
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
            norme = norm();
        }

        private double norm()
        {
            return Math.Sqrt( x * x + y * y + z * z);
        }

        public Vector normalization()
        {
            return this / norme;
        }

        public double dot(Vector vector)
        {
            return vector.x * x + vector.y * y + vector.z * z;
        }

        public Normal cross(Vector v)
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

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector operator *(double a, Vector b)
        {
            return new Vector(a * b.x, a * b.y, a * b.z);
        }

        public static Vector operator *(Vector b, double a)
        {
            return new Vector(a * b.x, a * b.y, a * b.z);
        }

        public static double operator *(Vector a, Vector b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static Vector operator /(Vector b, double a)
        {
            return new Vector(b.x / a, b.y / a, b.z / a);
        }

        public static Normal operator %(Vector a, Vector b)
        {
            return a.cross(b);
        }
    }
}
