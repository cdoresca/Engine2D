using _2D_engine.Algebre;
using _2D_engine.brdf;
using _2D_engine.Illumination;
using _2D_engine.Trace;

namespace _2D_engine.Materiel
{
    internal class Mirror : Phong
    {
        double kr;
        Couleur cr;
        PerfectSpecular perfectSpecular;

        public Mirror(Couleur cr, Couleur cd, double kr,double kd = 0.8, double ks = 0.2, double exp = 10) : base(cd,kd,ks,exp)
        {
            this.cr = cr;
            this.kr = kr;
            perfectSpecular = new PerfectSpecular(kr, cr);
        }

        public void setKr(double kr) { perfectSpecular.setCoeff(kr); }
        public void setCr(Couleur cr) { perfectSpecular.setCouleur(cr); }

        public override Couleur shade(Intersection info) 
        {
            Couleur phong = base.shade(info);

            Vecteur reflexion =new Vecteur();

            Couleur mirror = perfectSpecular.fSample(ref info,ref reflexion, -1*info.ray.directeur);

            Ray ray = new Ray(info.point, info.reflexion.normalization());

            Whitted trace = (Whitted)info.world.GetTracer();

            Couleur Li = trace.tracerRay(ray,info.depth+1);

            return phong + mirror * Li;

        }

    }
}
