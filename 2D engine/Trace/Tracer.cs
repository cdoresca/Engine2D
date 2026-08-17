using _2D_engine.Algebre;
using _2D_engine.Figure;
using _2D_engine.Illumination;

namespace _2D_engine.Trace
{
    internal abstract class Tracer
    {
        protected World world;
        public Tracer(World w) { world = w; }

        public abstract Couleur tracerRay(Ray ray);

        public Intersection Hit(Ray ray) 
        {

            Intersection best = new Intersection(world, false);


            double tmin = Double.MaxValue;

            foreach (Forme obj in world.GetFormes())
            {
                Intersection tmp = new Intersection(world, false);

                if (obj.Intersection(ray, ref tmp))
                {
                    if (tmp.t < tmin)
                    {
                        tmin = tmp.t;
                        best = tmp;

                        
                        if (best.normal * ray.directeur > 0)
                            best.normal = -1*best.normal;
                    }
                }
            }

            return best;
        }
        
        
    }
}
