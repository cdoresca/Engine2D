using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Algebre;
using _2D_engine.brdf;
using _2D_engine.Illumination;
using _2D_engine.Lights;
using _2D_engine.Trace;

namespace _2D_engine.Materiel
{
    internal class TextureMatte : Material
    {
        TextureDiffuse diffuse;
        double k;
        Texture.Texture tex;

        public TextureMatte(double k, Texture.Texture tex)
        {
            this.k = k;
            this.tex = tex;
            diffuse = new TextureDiffuse(k, tex);
        }

        public override Couleur GetRadiance()
        {
            return new Couleur();
        }

        public void setCoeff(double k) { this.k = k; }
        public void SetTexture(Texture.Texture k) { tex = k; }
        public override Couleur shade(Intersection info)
        {
            Couleur colorHit = new Couleur();

            foreach (Light light in info.world.GetLights())
            {
                light.Sample();
                Vecteur toLight = light.getDirection(info.point);


                if (!light.Shadow(new Ray(info.point, toLight), info))
                {

                    colorHit += diffuse.f(ref info, null, null) 
                        * light.getRadiance() 
                        * Math.Max(0, info.normal * toLight)
                        * light.Geo(info) / light.pdf();
                }
            }


            return colorHit;
        }
    }
}
