using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Algebre;
using _2D_engine.Figure;
using _2D_engine.Trace;

namespace _2D_engine
{
    internal class World
    {

        ViewPlane plane;
        public Couleur backgroundColor { get; set; }
        

        List<Forme> formeList = new List<Forme>();
        
        public World() { }
        public void Build()
        {
            plane = new ViewPlane();

            plane.width = 2000;
            plane.height = 2000;
            plane.pixelSize = 1.0;

            backgroundColor = new Couleur(Color.FromArgb(255, 0, 0, 0));

            foreach (var forme in formeList) { forme.world = this; }

            Console.WriteLine($"Scène construite avec {formeList.Count} forme(s).");
        }

        public Bitmap RenderScene()
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

                    ray = new Ray(new Algebre.Point(x, y, 100), new Vecteur(0, 0, -1));

                    colorPixel = TracerRay(ray);

                    img.SetPixel(i,j, colorPixel);
                }

            }

            return img;
        }

        public Color TracerRay(Ray ray) 
        {
            Color colorHit = backgroundColor.color;
            double tmin = ray.max;
            bool found = false; ;

            foreach (var item in formeList)
            {
                if (item.box.Intersects(ray))
                {
                    if (item.Intersection(ray, out Intersection info))
                    {
                        if (info.t < tmin && info.t > ray.min)
                        {
                            tmin = info.t;
                            colorHit = info.couleur * (ray.directeur * info.normal);
                            found = true;
                        }
                    }
                }
            }
            return found ? colorHit : backgroundColor.color;
        }

        public void AddForme(Forme f) { formeList.Add(f); }
    }
    
}
