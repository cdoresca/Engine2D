using _2D_engine.Algebre;

namespace _2D_engine.Acceleration
{
    internal class BoundingBox
    {

        public Point max { get; set; }

        public Point min { get; set; }

        public BoundingBox()
        {
            Random rnd = new Random();

            max = new Point(rnd.Next(), rnd.Next(), rnd.Next());
            min = new Point(rnd.Next(), rnd.Next(), rnd.Next());
        }

        public BoundingBox(Point p1, Point p2)
        {
            max = p1 > p2 ? p1 : p2;
            min = p1 > p2 ? p2 : p1;
        }

        public bool Overlaps(BoundingBox other)
        {
            for (int i = 0; i < 3; i++)
            {
                if (other.max[i] < min[i] || other.min[i] > max[i])
                {
                    return false;
                }
            }
            return true;

        }

        public bool Contains(Point point)
        {
            return point > min && point < max;
        }

        public bool Intersects(Ray r)
        {
            double t0, t1, tmax = r.max, tmin = r.min;

            for (int i = 0; i < 3; i++)
            {
                t0 = (min[i] - r.origine[i]) / r.directeur[i];
                t1 = (max[i] - r.origine[i]) / r.directeur[i];

                if (t0 > t1)
                {
                    (t0, t1) = (t1, t0);
                }

                tmin = Math.Max(t0, tmin);
                tmax = Math.Min(t1, tmax);
            }

            return tmax >= tmin;
        }

        public BoundingBox Combine(BoundingBox aabb)
        {
            Point p0 = min;
            Point p1 = max;

            for (int i = 0; i < 3; i++)
            {
                p0[i] = Math.Min(p0[i], aabb.min[i]);
                p1[i] = Math.Max(p1[i], aabb.max[i]);
            }

            return new BoundingBox(p0, p1);
        }
        public BoundingBox Combine(Point p)
        {

            Point p0 = new Point();
            Point p1 = new Point();

            for (int i = 0; i < 3; i++)
            {
                p0[i] = Math.Min(min[i], p[i]);
                p1[i] = Math.Max(max[i], p[i]);
            }

            return new BoundingBox(p0, p1);
        }

        public List<Point> Sommet()
        {
            List<Point> corners = new List<Point>();
            for (int i = 0; i < 8; ++i)
            {

                float x = (float)((i & 1) != 0 ? max[0] : min[0]);
                float y = (float)((i & 2) != 0 ? max[1] : min[1]);
                float z = (float)((i & 4) != 0 ? max[2] : min[2]);

                corners.Add(new Point(x, y, z));
            }
            return corners;
        }
    }
}
