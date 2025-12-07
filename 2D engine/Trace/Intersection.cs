using _2D_engine.Algebre;
using _2D_engine.Figure;
using _2D_engine.Illumination;
using _2D_engine.Materiel;

namespace _2D_engine.Trace
{
    internal struct Intersection
    {
       

        public Intersection(World world,bool h)
        {
            this.world = world;
            hit = h;
        }

        public World world;
        public double t;
        public Point point;
        public Normal normal;
        public Forme objet;
        public double u;
        public double v;
        public Ray ray;
        public bool hit;
        public Material material;
        public Vecteur reflexion;
        public int depth;
        
        public void SetInfo(double t, Point p, Normal n, Forme f, Material m, double u, double v, bool hit, Ray r)
        {
            this.u = u;
            this.v = v;
            this.t = t;

            point = p;
            normal = n;
            objet = f;
            material = m;
            ray = r;

            this.hit = hit;

        }
    }
}
