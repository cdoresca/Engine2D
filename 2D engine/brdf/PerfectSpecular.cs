using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Algebre;
using _2D_engine.Illumination;
using _2D_engine.Trace;

namespace _2D_engine.brdf
{
    internal class PerfectSpecular : BRDF
    {
        double k;
        Couleur color;

        public PerfectSpecular(double k, Couleur color)
        {
            this.color = color;
            this.k = k;
        }
        public void setCoeff(double k) { this.k = k; }
        public void setCouleur(Couleur k) { color = k; }


        public override Couleur f(ref Intersection info, Vecteur wi, Vecteur wo)
        {
            return color * k;
        }

        public override Couleur fSample(ref Intersection info, ref Vecteur wi, Vecteur wo)
        {
            info.reflexion = wi=-1 * wo + 2 * (info.normal * wo) * info.normal;
            return f(ref info, wi, wo) * (wo * info.normal) ;
        }
    }
}
