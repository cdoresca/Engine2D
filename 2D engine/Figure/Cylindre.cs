using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Algebre;
using _2D_engine.Trace;

namespace _2D_engine.Figure
{
    internal class Cylindre : Forme
    {
        double rayon,height;

        public Cylindre(double r,double h, GeomatricTransform t = null) 
        {
            rayon = r;
            height = h;

            Algebre.Point min = new Algebre.Point(centre[0] - rayon, centre[1] - height, centre[2] - rayon);
            Algebre.Point max = new Algebre.Point(centre[0] + rayon, centre[1] + height, centre[2] + rayon);
            box = new BoundingBox(min, max);
            transform = t ?? new GeomatricTransform();


        }

        public override Normal CalculNormal(Point point)
        {
            Vecteur n = (point - centre).normalization();
            return new Normal(n[0], n[1], n[2]);
        }

        public override bool Intersection(Ray ray, out Intersection info)
        {
            info = null;

            Ray localRay = GeomatricTransform.TransformRay(ray, transform.inverse);
            if (!box.Intersects(localRay)) return false;

            double a = localRay.directeur[0] * localRay.directeur[0] + localRay.directeur[2] * localRay.directeur[2];
            double b = 2 * (localRay.origine[0] * localRay.directeur[0] + localRay.origine[2] * localRay.directeur[2]);
            double c = localRay.origine[0] * localRay.origine[0] + localRay.origine[2] * localRay.origine[2] - rayon * rayon;

            double delta = b * b - 4 * a * c;

            if (delta < 0) { return false; }

            double t;

            t = (-b - Math.Sqrt(delta)) / (2 * a);


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
            Normal normalWorld = new Normal(GeomatricTransform.TransformNormal(CalculNormal(localHit), transform.inverse).normalization());
            info = new Intersection(t, pointWorld, normalWorld, this, this.color, (0, 0));


            return true;

        }

        public override double Surface()
        {
            return 2 * Math.PI * rayon * rayon + 2 *Math.PI * rayon * height;
        }
    }

}
