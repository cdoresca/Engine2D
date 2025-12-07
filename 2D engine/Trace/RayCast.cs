using _2D_engine.Algebre;
using _2D_engine.Figure;
using _2D_engine.Illumination;

namespace _2D_engine.Trace
{
    internal class RayCast : Tracer
    {
        public RayCast(World w) : base(w)
        {
        }

        public override Couleur tracerRay(Ray ray)
        {
            Intersection info = Hit(ray);

            return info.hit ? info.material.shade(info) : world.GetCouleur();
        }
    }
}
