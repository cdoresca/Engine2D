using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Algebre;

namespace _2D_engine.Figure
{
    internal abstract class Forme
    {
        public World world { get; set; }
        public abstract Color tracerRay(Ray ray);

        public abstract bool intersection(Ray ray);

        

    }
}
