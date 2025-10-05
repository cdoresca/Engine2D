using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_engine.Algebre
{
    internal class Ray
    {
        public Point origine { get; set; }
        public Vecteur directeur {  get; set; }

        public double min { get; set; }
        public double max { get; set; }

        int depth;
        public Ray(Point p, Vecteur v, double max = 500, double min = 25) 
        { 
            origine = p;
            directeur = v;
            this.min = min;
            this.max = max;
        }
    }
}
