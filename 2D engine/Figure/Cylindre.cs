using _2D_engine.Acceleration;
using _2D_engine.Algebre;
using _2D_engine.Trace;

namespace _2D_engine.Figure
{
    internal class Cylindre : Forme
    {
        double rayon, height;

        public Cylindre(double r, double h)
        {
            rayon = r;
            height = h;

            Algebre.Point min = new Algebre.Point(centre[0] - rayon, centre[1] - height, centre[2] - rayon);
            Algebre.Point max = new Algebre.Point(centre[0] + rayon, centre[1] + height, centre[2] + rayon);
            box = new BoundingBox(min, max);
            WorldBox = box;
            transform = new GeomatricTransform();


        }

        public override Normal GetNormal(Point point)
        {
            Vecteur n = new Vecteur(point[0] - centre[0], 0, point[2] - centre[2]);
            n = n.normalization();
            return new Normal(n[0], n[1], n[2]);
        }

        public override void GetUV(Point point, out double u, out double v)
        {
            throw new NotImplementedException();
        }

        public override bool Intersection(Ray ray, ref Intersection info)
        {
            

            Ray localRay = GeomatricTransform.TransformRay(ray, transform.inverse);

            double a = localRay.directeur[0] * localRay.directeur[0] + localRay.directeur[2] * localRay.directeur[2];
            double b = 2 * (localRay.origine[0] * localRay.directeur[0] + localRay.origine[2] * localRay.directeur[2]);
            double c = localRay.origine[0] * localRay.origine[0] + localRay.origine[2] * localRay.origine[2] - rayon * rayon;

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
            Algebre.Point pointWorld = GeomatricTransform.TransformPoint(localHit, transform.matrix);
            Normal normalWorld = new Normal(GeomatricTransform.TransformNormal(GetNormal(localHit), transform.inverse.GetTranspose()).normalization());
            info.SetInfo(t, pointWorld, normalWorld, this, material, 0, 0, true, ray);


            return true;

        }

        public override double pdf()
        {
            throw new NotImplementedException();
        }

        public override Point Sample()
        {
            throw new NotImplementedException();
        }

        public override double Surface()
        {
            return 2 * Math.PI * rayon * rayon + 2 * Math.PI * rayon * height;
        }

       
    }

}
