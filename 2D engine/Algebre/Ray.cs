namespace _2D_engine.Algebre
{
    internal class Ray
    {
        public Point origine { get; set; }
        public Vecteur directeur { get; set; }

        public double min { get; set; }
        public double max { get; set; }

        int depth;
        public Ray(Point p, Vecteur v, double max = 10000, double min = 1)
        {
            origine = p;
            directeur = v;
            this.min = min;
            this.max = max;
        }

        public Point at(double t)
        {
            return origine + t * directeur;
        }
    }
}
