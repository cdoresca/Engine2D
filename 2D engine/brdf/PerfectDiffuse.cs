using _2D_engine.Algebre;
using _2D_engine.Illumination;
using _2D_engine.Trace;

namespace _2D_engine.brdf
{
    internal class PerfectDiffuse : BRDF
    {
        double k;
        Couleur color;

        public PerfectDiffuse(double k, Couleur color)
        {
            this.k = k;
            this.color = color;
        }

        public void setCoeff(double k) {  this.k = k; }  
        public void setCouleur(Couleur k) {  color = k; }  



        public override Couleur f(ref Intersection info, Vecteur wi, Vecteur wo)
        {
            return k * color * (1.0/Math.PI);
        }

        public override Couleur fSample(ref Intersection info,ref Vecteur wi, Vecteur wo)
        {
            return k * color * (1.0/Math.PI);
        }
    }
}
