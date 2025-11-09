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

        public Couleur tracerRay(Ray ray) 
        {
            Couleur colorHit = new Couleur();
            double tmin = ray.max;
            bool found = false;

            if (world.GetAccelarator().Intersection(ray, out Intersection info))
            {
                foreach (Light light in world.GetLights())
                {
                    if (shadow(light,new Ray(light.getPosition(),light.getDirection(info.point)),info.objet))
                    {
                        colorHit += info.couleur * light.getRadiance() * Math.Abs(info.normal.norme / light.getDirection(info.point).norme);
                        found = true;
                    }
                }
            }
            return found ? colorHit : world.GetCouleur();
        }

        bool shadow(Light light, Ray ray, Forme forme)
        {
            double tmin = ray.max;
            Intersection info = null;

            foreach (var item in world.GetFormes())
            {
                if (item.boundingBox(ray))
                {
                    if(item.Intersection(ray,out info))
                    {
                        if(info.t < tmin)
                        {
                            tmin =info.t;
                            
                        }
                    }
                }
            }
            return info.objet == forme;
        }
    }
}
