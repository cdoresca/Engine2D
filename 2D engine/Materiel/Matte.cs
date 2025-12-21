using _2D_engine.Algebre;
using _2D_engine.brdf;
using _2D_engine.Illumination;
using _2D_engine.Lights;
using _2D_engine.Trace;

namespace _2D_engine.Materiel
{
    internal class Matte : Material
    {

        private double k;
        private Couleur color; 
        private PerfectDiffuse diffuse;

        public Matte(double k,Couleur c)
        {
            this.k = k;
            this.color = c;
            diffuse = new PerfectDiffuse(k, color);
        }

        public override Couleur GetRadiance()
        {
            return new Couleur();
        }

        public void setCoeff(double k)
        {
            diffuse.setCoeff(k);
        }

        public void setColor(Couleur c)
        {
            diffuse.setCouleur(c);
        }
        public override Couleur shade(Intersection info)
        {
            double EPS = 1e-4;

            Couleur colorHit = new Couleur();

            foreach (Light light in info.world.GetLights())
            {
                int n = 1;

                light.Sample();
                if (light is AreaLight area)
                    n = area.forme.sample.NbSamples;
                Couleur tmp = new Couleur();
                for (int i = 0; i < n; i++)
                {
                    

                        
                    Point origin = info.point + info.normal * EPS;

                    Vecteur toLight = light.getDirection(info.point);


                    if (!light.Shadow(new Ray(origin, toLight), info))
                    {

                        tmp += diffuse.f(ref info, null, null)
                            * light.getRadiance()
                            * Math.Max(0, info.normal * toLight)
                            * light.Geo(info) / light.pdf();
                    }
                }

                tmp /= n;
                colorHit += tmp;

            }
            
            
            return colorHit;
        }
    }
}
