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
        public Algebre.Point centre;
        public abstract bool Intersection(Ray ray, out Intersection info);

        
        public abstract double Surface();

        public void setCenter(Algebre.Point p) { centre = p; }








    }
}
