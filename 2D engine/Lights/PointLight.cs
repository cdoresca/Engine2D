using _2D_engine.Algebre;
using _2D_engine.Illumination;
using _2D_engine.Trace;

namespace _2D_engine.Lights
{
    internal class PointLight : Light
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
            position = p;
        }

        public override Point getPosition()
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

        public override double Geo(Intersection info)
        {
            return 1.0;
        }

        public override double pdf()
        {
            return 1.0;
        }

        public override void Sample()
        {
            
        }
    }
}
