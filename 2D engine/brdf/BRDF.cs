using _2D_engine.Algebre;
using _2D_engine.Illumination;
using _2D_engine.Trace;

namespace _2D_engine.brdf
{
    internal abstract class BRDF
    {
        public BRDF() { }

        public abstract Couleur f(ref Intersection info, Vecteur wi, Vecteur wo);
        public abstract Couleur fSample(ref Intersection info, ref Vecteur wi, Vecteur wo);
    }
}
