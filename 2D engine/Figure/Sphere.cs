using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Algebre;

namespace _2D_engine.Figure
{
    internal class Sphere : Forme
    {

        double rayon;
        Algebre.Point centre;
        

        

        public Sphere(Algebre.Point c,double r = 0)
        { 
            rayon = r;
            centre = c;
        }

        public void setCenter(Algebre.Point p) { centre = p; }

       

        public override Color tracerRay(Ray ray)
        {
            if (intersection(ray))
            {
                return Color.CadetBlue;
            }

            return world.backgroundColor;
        }
        
        public override bool intersection(Ray ray) 
        {
            double a, b, c;
            Vecteur l = ray.origine - centre;

            a = ray.directeur.dot(ray.directeur);
            b = 2 * l.dot(ray.directeur);
            c = l.dot(l) - rayon * rayon;

            double delta = b * b - 4 * a * c;

            if(delta < 0) { return false; }

            double t_0, t_1;

            t_0 = (- b - Math.Sqrt(delta)) / (2 * a);
            t_1 = (- b + Math.Sqrt(delta)) / (2 * a);

            if (t_0 < ray.min || t_0 > ray.max) 
            { 
                if (t_1 < ray.min || t_1 > ray.max)
                {
                    return false;
                }
            }

            if(ray.min < t_0 && t_0 < ray.max) { return true; }
            if(ray.min < t_1 && t_1 < ray.max) { return true; }

            return false;

        }

    
    }
}
