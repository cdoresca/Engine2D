using System.Drawing;
using _2D_engine.Algebre;
using _2D_engine.Illumination;

namespace _2D_engine.Camera
{
    internal class Pinhole : Camera
    {
        public override Vecteur GetDirection(double x, double y)
        {
            return (x * u + y * v - distance * w).normalization();

            
        }

        public override Algebre.Point GetPosition(double x, double y)
        {
            pixel = center;
            return pixel;
        }

    }
}
