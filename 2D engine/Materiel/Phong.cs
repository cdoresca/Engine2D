using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Algebre;
using _2D_engine.brdf;
using _2D_engine.Illumination;
using _2D_engine.Lights;
using _2D_engine.Trace;

namespace _2D_engine.Materiel
{
    internal class Phong : Material
    {

        double kd;
        Couleur cd;
        double ks;         
        double exponent;       
        PerfectDiffuse diffuse;
        GlossySpecular glossy;

        public Phong(Couleur cd,double kd = 0.8,double ks=0.2,double exp = 10)
        {
            this.kd = kd;
            this.ks = ks;
            this.cd = cd;
            this.exponent = exp;
            diffuse = new PerfectDiffuse(kd, cd);
            glossy = new GlossySpecular(ks, exponent);
        }

        

        public void setKd(double k)
        {
            diffuse.setCoeff(k);
        }

        public void setCd(Couleur c)
        {
           diffuse.setCouleur(c);
        }

        public void setKs(double k)
        {
            ks = k;
            glossy = new GlossySpecular(ks, exponent);
        }

        public void setExponent(double e)
        {
            exponent = e;
            glossy = new GlossySpecular(ks, exponent);
        }
        public override Couleur shade(Intersection info)
        {
            Couleur colorHit = new Couleur();
            double EPS = 1e-4;

            foreach (Light light in info.world.GetLights())
            {
                int n = 1;

                light.Sample();
                if (light is AreaLight area)
                    n = area.forme.sample.NbSamples;
                Couleur tmp = new Couleur();
                for (int i = 0; i < n; i++)
                {

                    Vecteur toLight = light.getDirection(info.point);
                    Point origin = info.point + info.normal * EPS;

                    if (!light.Shadow(new Ray(origin, toLight), info))
                    {
                        tmp += (((diffuse.f(ref info, toLight, info.ray.directeur) 
                            + glossy.f(ref info, toLight, -1 * info.ray.directeur))
                            * light.getRadiance()
                            * Math.Max(0, info.normal * toLight)
                            * light.Geo(info)) / light.pdf());
                       
                    }
                }
                tmp /= n;
                colorHit += tmp;
            }
            
            return colorHit;
        }

        public override Couleur GetRadiance()
        {
            return new Couleur();
        }
    }
}
