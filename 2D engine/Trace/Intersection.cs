using _2D_engine.Algebre;
using _2D_engine.Figure;

namespace _2D_engine.Trace
{
    internal class Intersection
    {
        public Intersection(double t, Algebre.Point p, Normal n, Forme f, Couleur c, (double, double) uv)
        {
            this.uv = uv;
            this.t = t;

            point = p;
            normal = n;
            objet = f;
            couleur = c;
        }

        public Intersection()
        {
        }

        public double t;
        public Algebre.Point point;
        public Normal normal;
        public Forme objet;
        public Couleur couleur;
        public (double, double) uv;

    }
}
