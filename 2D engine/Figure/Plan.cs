using _2D_engine.Acceleration;
using _2D_engine.Algebre;
using _2D_engine.Trace;
using _2D_engine.Sample;
using GT = _2D_engine.Algebre.GeomatricTransform;

namespace _2D_engine.Figure
{
    internal class Plan : Forme
    {
        Normal normal = new Normal(0, 1, 0);

        double width;
        double height;

        public Plan(double width = 1000, double height = 1000)
        {
            this.width = width;
            this.height = height;


            box = new BoundingBox(
                new Algebre.Point(centre[0] - width / 2, centre[1], centre[2] - height / 2),
                new Algebre.Point(centre[0] + width / 2, centre[1], centre[2] + height / 2)
            );
            WorldBox = box;
            transform = new GeomatricTransform();
            sample = new RandomSampler(4);
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

            Ray localRay = GT.TransformRay(ray, transform.inverse);

            double denom = normal * localRay.directeur;

            if (denom == 0) return false;

            double t = (centre - localRay.origine) * normal / denom;

            if (t < ray.min || t > ray.max)
                return false;

            Algebre.Point localHit = localRay.at(t);


            Algebre.Point pointWorld = GT.TransformPoint(localHit, transform.matrix);
            Normal normalWorld = new Normal(GT.TransformNormal(GetNormal(localHit), transform.inverse.GetTranspose()));


            info.SetInfo(t, pointWorld, normalWorld, this, material, 0, 0, true, ray);
            return true;
        }

        public override double pdf()
        {
            return 1 / Surface();
        }

        public override Point Sample()
        {
            Point unitSample = sample.sampleUnitSquare();

            double x  =  unitSample[0] * (box.max[0] - box.min[0]) + box.min[0];
            double z  =  unitSample[1] * (box.max[2] - box.min[2]) + box.min[2];

            Point planSample = GT.TransformPoint(new Point(x, 0, z), transform.matrix);

            return planSample;
        }

        public override double Surface()
        {
            return width * height;
        }

    }
}
