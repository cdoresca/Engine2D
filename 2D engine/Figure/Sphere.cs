using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        
        
        public Sphere(Algebre.Point c, GeomatricTransform t,double r = 100)
        { 
            rayon = r;
            centre = c;
            transform = t;
            box = new BoundingBox(
                GeomatricTransform.TransformPoint( new Algebre.Point(centre[0] - rayon, centre[1] - rayon, centre[2] - rayon),transform.matrix),
                GeomatricTransform.TransformPoint(new Algebre.Point(centre[0] + rayon, centre[1] + rayon, centre[2] + rayon), transform.matrix)
            );

        }

       
        
        public override bool Intersection(Ray ray, out Intersection info) 
        {
            info = null;

            Ray localRay = GeomatricTransform.TransformRay(ray, transform.inverse);
            double a, b, c;
            Vecteur l = localRay.origine - centre;

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
            Normal localNormal = CalculNormal(localHit);

            Algebre.Point pointWorld = GeomatricTransform.TransformPoint(localHit, transform.matrix);
            Normal normalWorld = GeomatricTransform.TransformNormal(localNormal, transform.inverse);

            info = new Intersection(t, pointWorld, normalWorld, this, this.color, GetUV(normalWorld));



            return true;
        }

        public Normal CalculNormal(Algebre.Point point) 
        {
            /**
            double theta = Math.Acos(point[1] * rayon);

            double cosPhi = point[2] / (rayon * Math.Sin(theta));
            double sinPhi = point[0] / (rayon * Math.Sin(theta));

            Vecteur dp_du = new Vecteur(2 * MathF.PI * point[2],0, -2 * MathF.PI * point[0]);
            Vecteur dp_dv = Math.PI * new Vecteur(point[1] * sinPhi, -rayon * Math.Sin(theta), point[1] * cosPhi);

            Vecteur normalizedVecteur = new Normal(dp_du, dp_dv).normalization();

            return new Normal(normalizedVecteur);
            **/

            Vecteur n = (point - centre).normalization();
            return new Normal(n[0], n[1], n[2]);
        }

        public override double Surface()
        {  
            return 4.0 * Math.PI * rayon * rayon;
        }

        public( double,double) GetUV(Normal n) 
        {
            double theta = Math.Acos(n[1]);              
            double phi = Math.Atan2(n[2], n[0]);      

            double u = (phi + Math.PI) / (2 * Math.PI); 
            double v = theta / Math.PI;

            return (u, v);
        }

    }
}

