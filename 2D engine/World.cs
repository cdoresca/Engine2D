using System.Drawing;
using _2D_engine.Acceleration;
using _2D_engine.Algebre;
using _2D_engine.Figure;
using _2D_engine.Illumination;
using _2D_engine.Lights;
using _2D_engine.Sample;
using _2D_engine.Trace;
using _2D_engine.Camera;
using _2D_engine.Materiel;
using _2D_engine.Texture;

namespace _2D_engine
{
    internal class World
    {
        ViewPlane plane;

        Couleur backgroundColor;

        List<Forme> formeList;

        List<Light> lights;

        Orthographic cameraOrtho;
        Pinhole cameraPinhole;
        Thinlens cameraThinlens;
        Fisheye cameraFisheye;

        
        Tracer tracer;

        public World() { Build(); }
        public void Build()
        {
            // ViewPlane
            plane = new ViewPlane();
            plane.width = 2000;
            plane.height = 2000;
            plane.pixelSize = 1.0;
            plane.maxDepth = 5;
            plane.sampler = new RandomSampler(4);

            // Fond d'écran
            backgroundColor = new Couleur();

            // Forme et Lumière
            formeList = new List<Forme>();
            lights = new List<Light>();
            AddObject();
            foreach (var forme in formeList) { forme.world = this; }
            foreach (var light in lights) { light.setWorld(this); } 

            // Tracer
            tracer = new RayCast(this);

            // Caméra
            cameraOrtho = new Orthographic();
            cameraOrtho.SetCenter(new Algebre.Point(0, 200, 200));
            cameraOrtho.SetLookAt(new Algebre.Point(0 , 0, 0));
            cameraOrtho.SetUp(new Vecteur(0, 1,0));
            cameraOrtho.SetViewPlaneDistance(200);
            cameraOrtho.SetWorld(this);
            cameraOrtho.setZoom(1);
            cameraOrtho.buildCSS();

            cameraPinhole = new Pinhole();
            cameraPinhole.SetCenter(new Algebre.Point(0, 300, 300));
            cameraPinhole.SetLookAt(new Algebre.Point(0, 0, 0));
            cameraPinhole.SetUp(new Vecteur(0, 1,0));
            cameraPinhole.SetViewPlaneDistance(500);
            cameraPinhole.SetWorld(this);
            cameraPinhole.setZoom(1);
            cameraPinhole.buildCSS();

            cameraThinlens = new Thinlens();
            cameraThinlens.SetCenter(new Algebre.Point(0, 0, 500));
            cameraThinlens.SetLookAt(new Algebre.Point(0, 0, 0));
            cameraThinlens.SetUp(new Vecteur(0, 1,0));
            cameraThinlens.SetFocalDistance(600);
            cameraThinlens.SetLensRadius(30);
            cameraThinlens.SetViewPlaneDistance(400);
            cameraThinlens.SetWorld(this);
            cameraThinlens.setZoom(1);
            cameraThinlens.buildCSS();

            cameraFisheye = new Fisheye();
            cameraFisheye.SetCenter(new Algebre.Point(0, 0, 500));
            cameraFisheye.SetLookAt(new Algebre.Point(0, 0, 0));
            cameraFisheye.SetUp(new Vecteur(0, 1, 0));
            cameraFisheye.SetViewPlaneDistance(100);
            cameraFisheye.SetPsiMax(180);
            cameraFisheye.SetWorld(this);
            cameraFisheye.setZoom(1);
            cameraFisheye.buildCSS();


            Console.WriteLine($"Scène construite avec {formeList.Count} forme(s), {lights.Count} Lumière(s).");
        }

        void AddObject()
        {
            Sphere sphere = new Sphere(50);
            Sphere sphere1 = new Sphere(100);
            Sphere earth = new Sphere(200);
            Plan plan = new Plan(300, 300);
            Plan sol = new Plan(3000,3000);

            sphere.AddTransform(GeomatricTransform.Translation(new Vecteur(0, 500, 0)));
            
            plan.AddTransform(GeomatricTransform.Translation(new Vecteur(0, 200, 0))
                           
                            );
            sol.AddTransform(GeomatricTransform.Translation(new Vecteur(0, -500,400))
                             //GeomatricTransform.RotationX(90)
                            );


            sphere.material = new IsotropicLight(10, new Couleur(Color.White));
            sphere1.material = new Phong(new Couleur(Color.Yellow));
            plan.material = new IsotropicLight(10,new Couleur(Color.White));
            sol.material = new Matte(1, new Couleur(Color.Cyan));

            earth.material = new TextureMatte(1, new ImageTexture("2k_earth_daymap.jpg"));

            
            
            List<Forme> formes = new List<Forme>();
            formes.AddRange(new Forme[] { sol,sphere1,plan});
            
            GridAccelarator accelarator = new GridAccelarator(formes);

            formeList.AddRange(new Forme[] { accelarator});

           
            lights.AddRange(new Light[] {
                new AreaLight(plan ,plan.material)
                //new PointLight(10,new Couleur(Color.White),new Algebre.Point(0,1000,0))
               
                
                

            });
            
        }



        public List<Forme> GetFormes() { return formeList; }

        public List<Light> GetLights() { return lights; }

        public Couleur GetCouleur() { return backgroundColor; }

        public ViewPlane GetView() { return plane; }

        public Tracer GetTracer() { return tracer; }

       

        internal Bitmap RenderScene()
        {
            return cameraOrtho.renderScene(this);
        }
    }
}
