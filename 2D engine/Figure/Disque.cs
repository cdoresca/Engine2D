using _2D_engine.Acceleration;
using _2D_engine.Algebre;
using _2D_engine.Trace;

namespace _2D_engine.Figure
{
    internal class Disque : Forme
    {
        Normal normal;
        double rayonInterieur;
        double rayonExterieur;

        public Disque(double ro = 300, double ri = 100)
        {
            normal = new Normal(0, 1, 0);
            rayonExterieur = ro;
            rayonInterieur = ri;
            Algebre.Point min = new Algebre.Point(centre[0] - rayonExterieur, centre[1], centre[2] - rayonExterieur);
            Algebre.Point max = new Algebre.Point(centre[0] + rayonExterieur, centre[1], centre[2] + rayonExterieur);
            box = new BoundingBox(min, max);
            WorldBox = box;
            transform = new GeomatricTransform();

        }
        public override Normal GetNormal(Point point)
        {
            return normal;
        }

        public override void GetUV(Point point, out double u, out double v)
        {
            throw new NotImplementedException();
        }

        public override bool Intersection(Ray ray, ref Intersection info)
        {

            Ray localRay = GeomatricTransform.TransformRay(ray, transform.inverse);

            double denom = normal * localRay.directeur;

            if (denom == 0) return false;

            double t = (centre - localRay.origine) * normal / denom;

            Algebre.Point localHit = localRay.at(t);

            double d = (localHit - centre).norme;

            if (d < rayonInterieur || d > rayonExterieur) return false;

            Algebre.Point pointWorld = GeomatricTransform.TransformPoint(localHit, transform.matrix);
            Normal normalWorld = GeomatricTransform.TransformNormal(GetNormal(localHit), transform.inverse.GetTranspose());
            normalWorld = new Normal(normalWorld.normalization());

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
            return Math.PI * (rayonExterieur * rayonExterieur - rayonInterieur * rayonInterieur);
        }

       
    }
}
