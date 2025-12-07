using _2D_engine.Figure;

namespace _2D_engine.Acceleration
{
    internal abstract class Accelarator : Forme
    {
        protected List<Forme> formes;


        public Accelarator(List<Forme> obj)
        {
            formes = obj;
        }

        public List<Forme> GetForme() { return formes; }


    }
}
