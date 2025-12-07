using _2D_engine.Acceleration;
using _2D_engine.Algebre;
using _2D_engine.Lights;
using _2D_engine.Sample;
using _2D_engine.Sample;
using _2D_engine.Trace;
using GT = _2D_engine.Algebre.GeomatricTransform;

namespace _2D_engine.Figure
{
    internal class Sphere : Forme
    {
        double rayon;


        public Sphere(double r = 100)
        {
            rayon = r;

            box = new BoundingBox(
                new Algebre.Point(centre[0] - rayon, centre[1] - rayon, centre[2] - rayon),
                new Algebre.Point(centre[0] + rayon, centre[1] + rayon, centre[2] + rayon)
            );
            WorldBox = box;
            transform = new GT();
            sample = new RandomSampler(4);
        }




        public override bool Intersection(Ray ray, ref Intersection info)
        {
            

            Ray localRay = GT.TransformRay(ray, transform.inverse);

            double a, b, c;
            Vecteur l = localRay.origine - centre;

            a = localRay.directeur * localRay.directeur;
            b = 2 * l * localRay.directeur;
            c = l * l - rayon * rayon;

            double delta = b * b - 4 * a * c;

            if (delta < 0) { return false; }

            double t;

            t = (-b - Math.Sqrt(delta)) / (2 * a);


            if (t < localRay.min || t > localRay.max)
            {
                t = (-b + Math.Sqrt(delta)) / (2 * a);

                if (t < localRay.min || t > localRay.max)
                {
                    return false;
                }
            }


            Algebre.Point localHit = localRay.at(t);
            GetUV(localHit, out double u, out double v);

            Algebre.Point pointWorld = GT.TransformPoint(localHit, transform.matrix);

            Normal normalObject = GetNormal(localHit);
            Normal normalWorld = GT.TransformNormal(normalObject, transform.inverse.GetTranspose());


            info.SetInfo(t, pointWorld, normalWorld, this, material, u, v, true,ray);



            return true;
        }
        public override double Surface()
        {
            return 4.0 * Math.PI * rayon * rayon;
        }

        public override Normal GetNormal(Algebre.Point point)
        {

            Vecteur n = (point - centre).normalization();
            return new Normal(n[0], n[1], n[2]);
        }

        public override void  GetUV(Point point, out double u, out double v)
        {
            point /= rayon;



            u = 0.5 + (Math.Atan2(point[2], point[0]) / (2.0 * Math.PI));
            v = 0.5 - (Math.Asin(point[1]) / Math.PI);
        }

        public override Point Sample()
        {
            Point unitSample = sample.sampleUnitSquare();
            
            double theta = Math.Acos(1 - 2 * unitSample[0]);
            double phi = 2 * Math.PI * unitSample[1];

            double x  =  rayon * Math.Sin(theta) * Math.Cos(phi);
            double y  =  rayon * Math.Sin(theta) * Math.Sin(phi);
            double z  =  rayon * Math.Cos(theta);

            Point planSample = GT.TransformPoint(new Point(x, y, z), transform.matrix);

            return planSample;
        }

        public override double pdf()
        {
            return 1 / Surface();
        }

       
    
    }
}

