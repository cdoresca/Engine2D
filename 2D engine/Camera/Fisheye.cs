using System.Drawing;
using _2D_engine.Algebre;
using _2D_engine.Illumination;
using _2D_engine.Sample;

namespace _2D_engine.Camera
{
    internal class Fisheye : Camera
    {

        double psi;
        

        public void SetPsiMax(double max) { psi = max * (Math.PI / 180.0); }

        public new Bitmap renderScene(World world)
        {

            Couleur colorPixel = new Couleur();
            ViewPlane plane = world.GetView();
            Ray ray;
            double x, y;
            Algebre.Point sample;
            Algebre.Point origin;
            Vecteur direction;


            plane.pixelSize /= zoom;

            Bitmap img = new Bitmap(plane.width, plane.height);

            for (int i = 0; i < plane.width; i++)
            {
                for (int j = 0; j < plane.height; j++)
                {
                    colorPixel = new Couleur();
                    for (int s = 0; s < plane.sampler.NbSamples; s++)
                    {
                        sample = plane.sampler.sampleUnitSquare();


                        x = 2 * (i + sample[0]) / plane.width - 1;
                        y = 2 * (j + sample[1]) / plane.height - 1;

                        origin = GetPosition(x, y);
                        direction = GetDirection(x,y);


                        if (direction == null) continue;
                        ray = new Ray(origin, direction);

                        colorPixel += world.GetTracer().tracerRay(ray);
                    }
                    colorPixel /= plane.sampler.NbSamples;

                    img.SetPixel(i, j, colorPixel.ToColor());
                }
            }
            return img;
        }

        public override Vecteur GetDirection(double x, double y)
        {
            double r = Math.Sqrt(x * x + y * y);
            if (r > 1) return null;

            double psiPixel = r * psi;

            double phi = Math.Atan2(y, x);

            double sinPsi = Math.Sin(psiPixel);
            double cosPsi = Math.Cos(psiPixel);

            return sinPsi * Math.Cos(phi) * u +
                   sinPsi * Math.Sin(phi) * v -
                   cosPsi * w;
        }

        public override Algebre.Point GetPosition(double x, double y)
        {
            pixel = new Algebre.Point(x, y);
            return pixel;
        }
    }
}
