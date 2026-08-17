using System.Drawing;
using System.Net;
using System.Numerics;
using _2D_engine.Algebre;
using _2D_engine.Illumination;

namespace _2D_engine.Camera
{
    internal class Thinlens : Camera
    {
        double rayon, focal;

        public Thinlens() { }
        public void SetFocalDistance(double focal) { this.focal = focal; }
        public void SetLensRadius(double r) {  this.rayon = r; }
        
       
        public override Vecteur GetDirection(double x, double y)
        {

            Algebre.Point pFocus = center + x * u + y * v - focal * w;


            Vecteur dir = (pFocus - pixel).normalization();

            return dir;
        }

        public override Algebre.Point GetPosition(double x, double y)
        {
            ViewPlane plane = world.GetView();
            
            Algebre.Point sampleLens = plane.sampler.sampleUnitCircle() * rayon;
            pixel = center + sampleLens[0] * u + sampleLens[1] * v;

            return pixel;
        }
    }
}
