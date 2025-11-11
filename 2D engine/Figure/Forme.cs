using _2D_engine.Acceleration;
using _2D_engine.Algebre;
using _2D_engine.Illumination;
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
        public BoundingBox WorldBox { get; set; }

        public Algebre.Point centre = new Algebre.Point(0, 0, 0);
        public abstract bool Intersection(Ray ray, out Intersection info);


        public abstract double Surface();

        public void setCenter(Point p) { centre = p; }

        public (double, double) GetUV(Normal n)
        {
            double theta = Math.Acos(n[1]);
            double phi = Math.Atan2(n[2], n[0]);

            double u = (phi + Math.PI) / (2 * Math.PI);
            double v = theta / Math.PI;

            return (u, v);
        }
        public abstract Normal GetNormal(Point point);


        public void AddTransform(params GT[] transforms)
        {
            foreach (GT t in transforms)
            {
                transform.Multiply(t);
            }
            
            
            Point min = new Point(double.MaxValue, double.MaxValue, double.MaxValue);
            Point max = new Point(double.MinValue, double.MinValue, double.MinValue);

            foreach (var c in box.Sommet())
            {
                var transformed = GT.TransformPoint(c, transform.matrix);

                for (int i = 0; i < 3; i++)
                {
                    min[i] = Math.Min(min[i], transformed[i]);
                    max[i] = Math.Max(max[i], transformed[i]);
                }
               
            }

            WorldBox = new BoundingBox(min, max);

        }

        public bool boundingBox(Ray ray)
        {
            
            return WorldBox.Intersects(ray);
        }

        public bool boundingBox(Ray ray , out double t)
        {
            
            return WorldBox.Intersects(ray, out t);
        }
    }
}
