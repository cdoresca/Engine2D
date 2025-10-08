using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Algebre;
using _2D_engine.Trace;

namespace _2D_engine.Figure
{
    internal class Sphere : Forme
    {
        double rayon;
        
        
        public Sphere(double r = 100)
        { 
            rayon = r;
         
            box = new BoundingBox(
                new Algebre.Point(centre[0] - rayon, centre[1] - rayon, centre[2] - rayon),
                new Algebre.Point(centre[0] + rayon, centre[1] + rayon, centre[2] + rayon)
            );
            transform = new GeomatricTransform();

        }




        public override bool Intersection(Ray ray, out Intersection info) 
        {
            info = null;

            Ray localRay = GeomatricTransform.TransformRay(ray, transform.matrix);

            if (!box.Intersects(localRay)) return false;

            double a, b, c;
            Vecteur l = new Vecteur(localRay.origine);

            a = localRay.directeur * localRay.directeur;
            b = 2 * l * localRay.directeur;
            c = l * l - rayon * rayon;

            double delta = b * b - 4 * a * c;

            if(delta < 0) { return false; }

            double t;

            t = (- b - Math.Sqrt(delta)) / (2 * a);
            

            if (t < localRay.min || t > localRay.max) 
            {
                t = (-b + Math.Sqrt(delta)) / (2 * a);

                if (t < localRay.min || t > localRay.max)
                {
                    return false;
                }
            }

            
            Algebre.Point localHit = localRay.at(t);
           

            Algebre.Point pointWorld = GeomatricTransform.TransformPoint(localHit, transform.inverse);
            Normal normalWorld = GeomatricTransform.TransformNormal(CalculNormal(localHit),transform.inverse);
            

            info = new Intersection(t, pointWorld, normalWorld, this, this.color, (0,0));



            return true;
        }
        public override double Surface()
        {  
            return 4.0 * Math.PI * rayon * rayon;
        }

        public override Normal CalculNormal(Algebre.Point point)
        {

            Vecteur n = (point - centre).normalization();
            return new Normal(n[0], n[1], n[2]);
        }


    }
}

