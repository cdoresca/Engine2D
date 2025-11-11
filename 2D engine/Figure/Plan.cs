using _2D_engine.Acceleration;
using _2D_engine.Algebre;
using _2D_engine.Trace;

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
        }

        public override Normal GetNormal(Point point)
        {
            return normal;
        }

        public override bool Intersection(Ray ray, out Intersection info)
        {
            info = null;

            Ray localRay = GeomatricTransform.TransformRay(ray, transform.inverse);

            double denom = normal * localRay.directeur;

            if (denom == 0) return false;

            double t = (centre - localRay.origine) * normal / denom;

            if (t < ray.min || t > ray.max)
                return false;

            Algebre.Point localHit = localRay.at(t);


            Algebre.Point pointWorld = GeomatricTransform.TransformPoint(localHit, transform.matrix);
            Normal normalWorld = new Normal(GeomatricTransform.TransformNormal(GetNormal(localHit), transform.inverse).normalization());


            info = new Intersection(t, pointWorld, normalWorld, this, this.color, (0, 0));
            return true;
        }

        public override double Surface()
        {
            return width * height;
        }
    }
}
