using _2D_engine.Algebre;
using _2D_engine.Trace;
using GT = _2D_engine.Algebre.GeomatricTransform;

namespace _2D_engine.Figure
{
    internal abstract class Forme
    {

        public GT transform { get; set; }
        public Couleur color { get; set; }
        public World world { get; set; }
        public BoundingBox box { get; set; }
        public Algebre.Point centre = new Algebre.Point(0, 0, 0);
        public abstract bool Intersection(Ray ray, out Intersection info);


        public abstract double Surface();

        public void setCenter(Algebre.Point p) { centre = p; }

        public (double, double) GetUV(Normal n)
        {
            double theta = Math.Acos(n[1]);
            double phi = Math.Atan2(n[2], n[0]);

            double u = (phi + Math.PI) / (2 * Math.PI);
            double v = theta / Math.PI;

            return (u, v);
        }
        public abstract Normal CalculNormal(Algebre.Point point);


        public void AddTransform(params GT[] transforms)
        {
            foreach (GT t in transforms)
            {
                transform.Multiply(t);
            }

        }

        public bool boundingBox(Ray ray)
        {
            Ray localRay = GT.TransformRay(ray, transform.matrix);
            if (!box.Intersects(localRay)) return false;
            return true;
        }
        public bool boundingBox(Ray ray, out double t)
        {
            Ray localRay = GT.TransformRay(ray, transform.matrix);
            if (!box.Intersects(localRay, out double tmin))
            {
                t = tmin;
                return false;
            }
            t = tmin;
            return true;
        }
    }
}
