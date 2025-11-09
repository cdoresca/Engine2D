using _2D_engine.Algebre;
using _2D_engine.Figure;
using _2D_engine.Illumination;

namespace _2D_engine.Trace
{
    internal class Intersection
    {
        public Intersection(double t,Point p, Normal n, Forme f, Couleur c, (double, double) uv)
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
        public Point point;
        public Normal normal;
        public Forme objet;
        public Couleur couleur;
        public (double, double) uv;

    }
}
