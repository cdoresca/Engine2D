using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Illumination;
using _2D_engine.Trace;

namespace _2D_engine.Texture
{
    internal class ConstantTexture : Texture
    {
        Couleur color;
        public ConstantTexture(Couleur c) { color = c; }

        public void SetColor(Couleur c) { color = c; }

        public override Couleur GetCouleur(Intersection info)
        {
            return color;
        }
    }
}
