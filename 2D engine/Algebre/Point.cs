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

        public static Vector operator -(Point b, Point a)
        {
            return new Vector(a.x - b.x, a.y - b.y, a.z - b.z);
        }
    }
}
