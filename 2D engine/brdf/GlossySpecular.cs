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
    internal class GlossySpecular : BRDF
    {
        double k, exp;

        public GlossySpecular(double k, double exp)
        {
            this.k = k;
            this.exp = exp;
        }

        public void setCoeff(double k) { this.k = k; }
        public void setExp(double exp) {  this.exp = exp; }
        public override Couleur f(ref Intersection info, Vecteur wi, Vecteur wo)
        {
            Vecteur r = -1 * wi + 2 * (info.normal * wi) * info.normal;
            return k * Math.Pow(r * wo,exp) * new Couleur(1,1,1,1);
        }

        public override Couleur fSample(ref Intersection info, ref Vecteur wi, Vecteur wo)
        {
            return f(ref info,wi,wo);
        }
    }
}
