using _2D_engine.Algebre;
using _2D_engine.Trace;

namespace _2D_engine.Illumination
{
    internal class PointLight :Light
    {
        double ls;
       
        public PointLight() 
        {
            ls = 1;
            position = new Point();
            color = new Couleur();

        }

        public PointLight(double ls, Couleur color, Point p) 
        { 
            this.ls = ls;
            this.color = color;
            this.position = p;
        }

        public override Algebre.Point getPosition()
        {
            return position;
        }

        public override Couleur getRadiance()
        {
            return ls * color;
        }

        public override Vecteur getDirection(Point point) 
        {
            return (position - point).normalization();
        }
    }
}
