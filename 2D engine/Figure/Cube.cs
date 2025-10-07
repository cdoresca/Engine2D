using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Algebre;
using _2D_engine.Trace;

namespace _2D_engine.Figure
{
    internal class Cube : Forme
    {
        double size;
        Algebre.Point min;
        Algebre.Point max;
        public Cube( double s = 100)
        {
            size = s;
            double half = size / 2;
            min = new Algebre.Point(centre[0] - half, centre[1] - half, centre[2] - half);
            max = new Algebre.Point(centre[0] + half, centre[1] + half, centre[2] + half);
            box = new BoundingBox(min, max);
            transform =  new GeomatricTransform();
        }

        public override bool Intersection(Ray ray, out Intersection info)
        {
            info = null;

            Ray localRay = GeomatricTransform.TransformRay(ray, transform.matrix);

            if (!box.Intersects(localRay)) return false;

            double t0, t1, tmax = localRay.max, tmin = localRay.min;

            for (int i = 0; i < 3; i++)
            {
                t0 = (min[i] - localRay.origine[i]) / localRay.directeur[i];
                t1 = (max[i] - localRay.origine[i]) / localRay.directeur[i];

                if (t0 > t1)
                {
                    (t0, t1) = (t1, t0);
                }

                tmin = Math.Max(t0, tmin);
                tmax = Math.Min(t1, tmax);
            }

            if (tmax < tmin) return false;

            

            double t = tmin;

            Algebre.Point localHit = localRay.at(t);

            
            Normal localNormal = CalculNormal(localHit);

           
            Algebre.Point pointWorld = GeomatricTransform.TransformPoint(localHit, transform.inverse);
            Vecteur vecteurWorld = GeomatricTransform.TransformNormal(localNormal, transform.inverse.GetTranspose()).normalization();
            Normal normalWorld = new Normal(vecteurWorld);
            if (normalWorld* ray.directeur > 0) normalWorld = new Normal(-1*normalWorld);
            info = new Intersection(t, pointWorld, normalWorld, this, this.color, GetUV(normalWorld));

            return true;
        }

        

        public override double Surface()
        {
            return 6 * size * size; 

        }

        public override Normal CalculNormal(Algebre.Point point)
        {
            Vecteur n = (point - centre).normalization();
            return new Normal(n[0], n[1], n[2]);
        }
    }
}
