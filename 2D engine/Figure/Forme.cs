using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Algebre;
using _2D_engine.Trace;

namespace _2D_engine.Figure
{
    internal abstract class Forme
    {
       
        public GeomatricTransform transform { get; set; }
        
        public Couleur color { get; set; }
        public World world { get; set; }
        public BoundingBox box { get; set; }
        public Algebre.Point centre = new Algebre.Point(0,0,0);
        public abstract bool Intersection(Ray ray, out Intersection info);

        
        public abstract double Surface();

        public void setCenter(Algebre.Point p) { centre = p; }

        public (double, double) GetUV(Normal n)
        {
            double theta = Math.Acos(n[1]);
            double phi = Math.Atan2(n[2], n[0]);

            double u = (phi + Math.PI) / (2 * Math.PI);
            double v = theta / Math.PI;

            return (u, v);
        }
        public abstract Normal CalculNormal(Algebre.Point point);
       

        public void AddTransform(params GeomatricTransform[] transforms)
        {
            foreach (GeomatricTransform t in transforms)
            {
                transform.Multiply(t);
            }

        }

    }
}
