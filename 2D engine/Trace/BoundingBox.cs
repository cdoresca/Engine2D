using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Algebre;

namespace _2D_engine.Trace
{
    internal class BoundingBox
    {

        Algebre.Point max;

        Algebre.Point min;

        public BoundingBox()
        {
            Random rnd = new Random();

           max = new Algebre.Point(rnd.Next(), rnd.Next(),rnd.Next());
           min = new Algebre.Point(rnd.Next(), rnd.Next(),rnd.Next());
        }

        public BoundingBox(Algebre.Point p1, Algebre.Point p2) 
        {
            max = p1 > p2 ?p1 : p2;
            min = p1 < p2 ?p1 : p2
                ;
        }

        public bool Overlaps(BoundingBox other) 
        {
            return other.min > min || other.max < max || other.max <= max && other.min < max || other.min >= min && other.max > min;
        }

        public bool Contains(Algebre.Point point) 
        {
            return point > min && point < max;
        }

        public bool Intersects(Ray r) 
        {
            double t0, t1, tmax = r.max, tmin = r.min;

            for (int i = 0; i < 3; i++) 
            {
                t0 = (min[i] - r.origine[i]) / r.directeur[i];
                t1 = (max[i] - r.origine[i]) / r.directeur[i];

                if (t0 > t1)
                {
                    (t0,t1) =(t1,t0);
                }

                tmin = Math.Max(t0,tmin);
                tmax = Math.Min(t1,tmax);
            }

            return tmax > tmin;
        }

        BoundingBox Combine(BoundingBox aabb) 
        {
            Algebre.Point p0 = min;
            Algebre.Point p1 = max;

            for (int i = 0; i < 3; i++)
            {
                p0[i] = Math.Min(p0[i], aabb.min[i]);
                p1[i] = Math.Max(p1[i], aabb.max[i]);
            }

            return new BoundingBox(p0, p1);
        }
        BoundingBox Combine(Algebre.Point p) 
        {
            Algebre.Point p0 = min;
            Algebre.Point p1 = max;

            for (int i = 0; i < 3; i++)
            {
                p0[i] = Math.Min(p0[i],p[i]);
                p1[i] = Math.Max(p1[i],p[i]);
            }

            return new BoundingBox(p0,p1);
        }
    }
}
