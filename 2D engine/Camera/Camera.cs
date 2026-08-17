using System.Drawing;
using _2D_engine.Algebre;
using _2D_engine.Illumination;

namespace _2D_engine.Camera
{
    internal abstract class Camera
    {
        protected Algebre.Point center, lookAt, pixel;
        protected Vecteur up, u, v, w;
        protected double zoom, distance;
        protected World world;

        public Camera() { }

        public void buildCSS()
        {
            w = (center - lookAt).normalization();
            u = (w % up).normalization();
            v = (Vecteur)u % w;

            if (Math.Abs(center[0] - lookAt[0]) < Double.Epsilon
                && Math.Abs(center[2] - lookAt[2]) < Double.Epsilon)
            {
                if (center[1] > lookAt[1])
                {
                    u = new Vecteur(0, 0, 1);
                    v = new Vecteur(1, 0, 0);
                    w = new Vecteur(0, 1, 0);
                }
                if (center[1] < lookAt[1])
                {
                    u = new Vecteur(0, 0, 1);
                    v = new Vecteur(1, 0, 0);
                    w = new Vecteur(0, -1, 0);
                }
            }
        }

        public void SetCenter(Algebre.Point p) { center = p; }
        public void SetLookAt(Algebre.Point p) { lookAt = p; }
        public void SetViewPlaneDistance(double dis) { distance = dis; }
        public void setZoom(double z) {  zoom = z; }
        public void SetUp(Vecteur u) {  up = u; }
        public  void SetWorld(World w) {  world = w; }
        public abstract Vecteur GetDirection(double x, double y);
        public abstract Algebre.Point GetPosition(double x,double y);
        public Bitmap renderScene(World world)
        {
            Couleur colorPixel;
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

                        x = plane.pixelSize * (i - plane.width * 0.5 + sample[0]);
                        y = -plane.pixelSize * (j - plane.height * 0.5 + sample[1]);

                        origin = GetPosition(x, y);
                        direction = GetDirection(x,y);
                        
                        ray = new Ray(origin, direction);

                        colorPixel += world.GetTracer().tracerRay(ray);
                    }
                    colorPixel /= plane.sampler.NbSamples;

                    img.SetPixel(i, j, colorPixel.ToColor());
                    
                }
            }
            return img;
        }
    }
}
