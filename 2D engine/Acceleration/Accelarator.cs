using _2D_engine.Algebre;
using _2D_engine.Figure;
using _2D_engine.Trace;

namespace _2D_engine.Acceleration
{
    internal abstract class Accelarator
    {
        protected List<Forme> formes;

        
        public Accelarator(List<Forme> obj)
        {
            formes = obj;
        }

        public abstract bool Intersection(Ray ray, out Intersection info);


    }
}
