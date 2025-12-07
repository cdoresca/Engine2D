using _2D_engine.Illumination;
using _2D_engine.Trace;

namespace _2D_engine.Materiel
{
    internal abstract class Material
    {
        public abstract Couleur shade(Intersection info);
        public abstract Couleur GetRadiance();
    }
}
