using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Algebre;
using _2D_engine.Trace;

namespace _2D_engine.Figure
{
    internal class Triangle : Forme
    {
        double width;
        double height;
        double density;

        Algebre.Point p1;
        Algebre.Point p2;
        Algebre.Point p3;
        
       
        public Triangle(Algebre.Point a, Algebre.Point b, Algebre.Point c)
        {
            p1 = a; p2 = b; p3 = c;

            box = new BoundingBox(p1, p2);
            box = box.Combine(p3);

            transform = new GeomatricTransform();
        }
        public Triangle(double width, double height)
        {
            this.width = width;
            this.height = height;
            density = 100;

            p1 = new Point(centre[0] - width / 2, centre[1], centre[2] + height / 2);
            p2 = new Point(centre[0] + width / 2, centre[1], centre[2] + height / 2);
            p3 = new Point(centre[0], centre[1], centre[2] - height / 2);

            Algebre.Point min = new Point(Math.Min(p1[0], Math.Min(p2[0], p3[0])), Math.Min(p1[1], Math.Min(p2[1], p3[1])) - density,
                Math.Min(p1[2], Math.Min(p2[2], p3[2])));

            Algebre.Point max = new Point(Math.Max(p1[0], Math.Max(p2[0], p3[0])), Math.Max(p1[1], Math.Max(p2[1], p3[1])) + density,
                Math.Max(p1[2], Math.Max(p2[2], p3[2])));

            box = new BoundingBox(min, max);
           
            transform = new GeomatricTransform();
        }

        public override Normal CalculNormal(Point point)
        {
            Vecteur v = (p2 - p1) % (p3 - p1);

            return new Normal(v / v.norme);
        }

        public override bool Intersection(Ray ray, out Intersection info)
        {
            info = null;

            Ray localRay = GeomatricTransform.TransformRay(ray, transform.matrix);
            if (!box.Intersects(localRay)) return false;

            
            Vecteur dir = localRay.directeur;
            Vecteur rhs = p1 - localRay.origine;

            Matrice3x3 M = new Matrice3x3(p1 - p2, p1 - p3, dir);
            Vecteur sol = SystemeEquation.Cramer(M, rhs);

            double beta = sol[0];
            double gamma = sol[1];
            double t = sol[2];

            if (beta < 0 ||gamma < 0 || beta + gamma > 1|| t <= localRay.min || t >= localRay.max) return false;

            Point localHit = localRay.at(t);
            Normal localNormal = CalculNormal(localHit);

            Point pointWorld = GeomatricTransform.TransformPoint(localHit, transform.inverse);
            Vecteur vecteurWorld = GeomatricTransform.TransformNormal(localNormal, transform.inverse.GetTranspose()).normalization();
            Normal normalWorld = new Normal(vecteurWorld);

            info = new Intersection(t, pointWorld, normalWorld, this, this.color, (0, 0));
            return true;
        }

        public override double Surface()
        {
            return 1/2 *((p2 - p1)%(p3 - p1)).norme;
        }
    }
}
