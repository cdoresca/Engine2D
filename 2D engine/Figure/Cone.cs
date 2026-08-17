using _2D_engine.Acceleration;
using _2D_engine.Algebre;
using _2D_engine.Trace;

namespace _2D_engine.Figure
{
    internal class Cone : Forme
    {
        double rayon;
        double hauteur;
        public Cone(double r, double h)
        {
            rayon = r;
            hauteur = h;

            Algebre.Point min = new Algebre.Point(centre[0] - rayon, centre[1] - hauteur, centre[2] - rayon);
            Algebre.Point max = new Algebre.Point(centre[0] + rayon, centre[1] + hauteur, centre[2] + rayon);
            box = new BoundingBox(min, max);
            WorldBox = box;
            transform = new GeomatricTransform();
        }

        public override Normal GetNormal(Point point)
        {
            double k = rayon / hauteur;
            return new Normal(point[0], -k * k * point[1], point[2]);
        }

        public override void GetUV(Point point, out double u, out double v)
        {
            throw new NotImplementedException();
        }

        public override bool Intersection(Ray ray, ref Intersection info)
        {
            Ray localRay = GeomatricTransform.TransformRay(ray, transform.inverse);


            double k = rayon / hauteur;

            double a = localRay.directeur[0] * localRay.directeur[0] + localRay.directeur[2] * localRay.directeur[2]
                - k * k * localRay.directeur[1] * localRay.directeur[1];
            double b = 2 * (localRay.directeur[0] * localRay.origine[0] + localRay.directeur[2] * localRay.origine[2]
                - k * k * localRay.directeur[1] * localRay.origine[1]);
            double c = localRay.origine[0] * localRay.origine[0] + localRay.origine[2] * localRay.origine[2]
                - k * k * localRay.origine[1] * localRay.origine[1];

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

            if (localHit[1] < 0 || localHit[1] > hauteur)
                return false;

            Normal localNormal = GetNormal(localHit).normalization();


            Algebre.Point pointWorld = GeomatricTransform.TransformPoint(localHit, transform.matrix);
            Vecteur vecteurWorld = GeomatricTransform.TransformNormal(localNormal, transform.inverse.GetTranspose()).normalization();
            Normal normalWorld = new Normal(vecteurWorld);
            if (normalWorld * ray.directeur > 0) normalWorld = new Normal(-1 * normalWorld);
            info.SetInfo(t, pointWorld, normalWorld, this, material, 0, 0, true,ray);

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
            return Math.PI * rayon * rayon + 2 * Math.PI * rayon * hauteur;
        }

    }
}
