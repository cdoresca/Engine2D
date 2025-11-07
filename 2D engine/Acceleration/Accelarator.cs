using _2D_engine.Algebre;
using _2D_engine.Figure;
using _2D_engine.Trace;

namespace _2D_engine.Acceleration
{
    internal class Accelarator : Forme
    {
        protected List<Forme> formes;
        public Accelarator(List<Forme> obj)
        {
            formes = obj;
        }
        public override Normal CalculNormal(Point point)
        {
            throw new NotImplementedException();
        }

        public override bool Intersection(Ray ray, out Intersection info)
        {
            info = null;
            return false;
        }

        public override double Surface()
        {
            throw new NotImplementedException();
        }


    }
}
