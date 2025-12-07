using System;
using System.Drawing;
using _2D_engine.Algebre;
using _2D_engine.Illumination;

namespace _2D_engine.Camera
{
    internal class Orthographic : Camera
    {
        public override Vecteur GetDirection(double x, double y)
        {
            return -1 * w;
            
        }

        public override Algebre.Point GetPosition(double x, double y)
        {
            pixel =new Algebre.Point(x, y,distance);

            return pixel;
        }

        
    }
}
