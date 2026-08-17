using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Illumination;
using _2D_engine.Trace;

namespace _2D_engine.Texture
{
    internal abstract class Texture
    {
        public Texture() { }

        public abstract Couleur GetCouleur(Intersection info);

    }
}
