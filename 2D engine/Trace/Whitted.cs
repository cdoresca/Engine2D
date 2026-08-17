using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Algebre;
using _2D_engine.Figure;
using _2D_engine.Illumination;

namespace _2D_engine.Trace
{
    internal class Whitted : Tracer
    {
        public Whitted(World w) : base(w)
        {
        }

        public override Couleur tracerRay(Ray ray)
        {
            return tracerRay(ray, 0);
        }
        public  Couleur tracerRay(Ray ray, int depth)
        {
            if(depth > world.GetView().maxDepth)
            {
                return world.GetCouleur();
            }

            Intersection info = Hit(ray);
            if (!info.hit) return world.GetCouleur();

            info.depth = depth;

            return info.material.shade(info);

        }
    }
}
