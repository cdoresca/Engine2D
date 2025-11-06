using System.Drawing;
using _2D_engine.Algebre;
using _2D_engine.Figure;
using _2D_engine.Trace;

namespace _2D_engine
{
    internal class World
    {

        ViewPlane plane;
        public Couleur backgroundColor { get; set; }


        List<Forme> formeList;

        public World() { }
        public void Build()
        {
            plane = new ViewPlane();

            plane.width = 2000;
            plane.height = 2000;
            plane.pixelSize = 1.0;

            backgroundColor = new Couleur(Color.FromArgb(255, 0, 0, 0));

            AddForme();

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

                    img.SetPixel(i, j, colorPixel);
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
                if (item.boundingBox(ray))
                {

                    if (item.Intersection(ray, out Intersection info))
                    {
                        if (info.t < tmin)
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
        public void AddForme()
        {
            formeList = new List<Forme>();

            Sphere sphere = new Sphere(200);
            Plan plan = new Plan();
            Cube cube = new Cube(600);
            Cylindre cylindre = new Cylindre(100, 300);
            Disque disque = new Disque(400, 200);
            Cone cone = new Cone(150, 300);
            Triangle triangle = new Triangle(500, 300);

            sphere.AddTransform(GeomatricTransform.Translation(new Vecteur(0, 0, 0)));
            plan.AddTransform(GeomatricTransform.RotationX(25));
            cube.AddTransform(new GeomatricTransform[] { GeomatricTransform.RotationX(45), GeomatricTransform.RotationY(45) });
            cylindre.AddTransform(GeomatricTransform.Scale(2, 1, 1));
            disque.AddTransform(GeomatricTransform.Rotation(45, new Vecteur(1, 1, 0)));
            triangle.AddTransform(GeomatricTransform.RotationX(25));

            formeList.AddRange(new Forme[] { cube });

            sphere.color = new Couleur(Color.Red);
            plan.color = new Couleur(Color.Red);
            cube.color = new Couleur(Color.Red);
            cylindre.color = new Couleur(Color.Red);
            disque.color = new Couleur(Color.Red);
            cone.color = new Couleur(Color.Red);
            triangle.color = new Couleur(Color.Red);

        }

    }



}
