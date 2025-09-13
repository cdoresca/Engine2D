using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Algebre;
using _2D_engine.Forme;

namespace _2D_engine
{
    internal class World
    {

        ViewPlane plane;
        public Color backgroundColor { get; set; }
        Tracer tracer;
        Sphere sphere;


        public World() { }
        public void build()
        {
            plane = new ViewPlane();

            plane.width = 2000;
            plane.height = 2000;
            plane.pixelSize = 1.0;

            backgroundColor = Color.FromArgb(255, 0, 0, 0);

            sphere = new Sphere(500);
            sphere.setCenter(new Algebre.Point(0, 0, 0));
            sphere.world = this;
            tracer = sphere;

        }

        public Bitmap renderScene()
        {
            Color colorPixel;

            Ray ray;

            double x, y;

            Bitmap img = new Bitmap(plane.width, plane.height);

            for (int i = 0; i < plane.width; i++)
            {
                for (int j = 0; j < plane.height; j++)
                {

                    x = plane.pixelSize * (i - (plane.width + 1) * 0.5);
                    y = plane.pixelSize * (j - (plane.height + 1) * 0.5);

                    ray = new Ray(new Algebre.Point(x, y, 100), new Vector(0, 0, -1));

                    colorPixel = tracer.tracerRay(ray);

                    img.SetPixel(i,j, colorPixel);
                }

            }

            return img;
        }


    }
    
}
