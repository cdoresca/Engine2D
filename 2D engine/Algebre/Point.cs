using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_engine.Algebre
{
    internal class Point
    {

        double x;
        double y;
        double z;

        public Point(double x = 0, double y = 0, double z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double Distance(Point other)
        { 
            return (other - this).norme;
        }

        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }

        public static Vecteur operator -(Point a, Point b)
        {
            return new Vecteur(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static bool operator <(Point a, Point b) 
        {
            return  a.x < b.x && a.y < b.y && a.z<b.z;
        }
        public static bool operator >(Point a, Point b) 
        {
            return a.x > b.x && a.y > b.y && a.z > b.z; ;
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z; ;
        }
        public static bool operator !=(Point a, Point b)
        {
            return a.x != b.x && a.y != b.y && a.z != b.z;
        }

        public static bool operator <=(Point a, Point b)
        {
            return a.x < b.x || a.y < b.y || a.z < b.z;
        }
        public static bool operator >=(Point a, Point b)
        {
            return a.x > b.x || a.y > b.y || a.z > b.z; ;
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
