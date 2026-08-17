using _2D_engine.Algebre;
using _2D_engine.Figure;
using _2D_engine.Trace;

namespace _2D_engine.Acceleration
{
    internal class Cell
    {
        List<Forme> objects;
        public Cell() { objects = new List<Forme>(); }

        public void Add(Forme forme) { objects.Add(forme); }

        public int Count { get { return objects.Count; } }

        public bool Intersect(Ray ray, ref Intersection info)
        {

            

            if (objects.Count == 0)
                return false;

            double tmin = ray.max;
            bool found = false;


            foreach (var item in objects)
            {
                if (item.boundingBox(ray))
                {

                    if (item.Intersection(ray,ref info))
                    {

                        if (info.t < tmin)
                        {
                            tmin = info.t;
                            found = true;
                            
                        }
                    }
                }

            }
            return found;
        }

    }
}
