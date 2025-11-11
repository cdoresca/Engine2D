using _2D_engine.Acceleration;
using _2D_engine.Algebre;
using _2D_engine.Figure;
using _2D_engine.Trace;

namespace _2D_engine.Illumination
{
    internal class DirectIllumination
    {
        World world;

        public DirectIllumination(World w) 
        { 
            world = w;
        }

        public Couleur TracerRay(Ray ray)
        {
            if (!world.GetAccelarator().Intersection(ray, out Intersection info))
                return world.GetCouleur();

            Couleur colorHit = new Couleur();

            foreach (Light light in world.GetLights())
            {
                Vecteur toLight = light.getDirection(info.point);
                

                if (!shadow(light, new Ray(info.point, toLight), info.objet))
                {
                    
                    colorHit += info.couleur * light.getRadiance() * Math.Abs(info.normal * toLight);
                }
            }

            return colorHit;
        }

        bool shadow(Light light, Ray ray, Forme forme)
        {

            
            Vecteur toLight = light.getPosition() - ray.origine;

            foreach(var item in world.GetFormes()) {
                if (item == forme) continue;

                if (!item.boundingBox(ray)) continue;
                
                if(item.Intersection(ray,out Intersection info))
                {
                    if (info.t < toLight.norme) return true;
                }
                
            
            }
            return false;
        }
    }
}
