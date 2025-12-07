using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Algebre;
using _2D_engine.Illumination;
using _2D_engine.Trace;

namespace _2D_engine.brdf
{
    internal class TextureDiffuse : BRDF
    {

        double k;
        Texture.Texture tex;

        public TextureDiffuse(double k, Texture.Texture tex)
        {
            this.k = k;
            this.tex = tex;
        }

        public void setCoeff(double k) { this.k = k; }
        public void SetTexture(Texture.Texture k) { tex = k; }
        public override Couleur f(ref Intersection info, Vecteur wi, Vecteur wo)
        {
            return k * tex.GetCouleur(info) / Math.PI;
        }

        public override Couleur fSample(ref Intersection info, ref Vecteur wi, Vecteur wo)
        {
            return f(ref info,wi,wo);
        }
    }
}
