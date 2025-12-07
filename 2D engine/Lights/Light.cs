using _2D_engine.Algebre;
using _2D_engine.Figure;
using _2D_engine.Acceleration;
using _2D_engine.Illumination;
using _2D_engine.Trace;

namespace _2D_engine.Lights
{
    internal abstract class Light
    {

        protected Point position;
        protected Couleur color;
        protected World world;
        public Light() { }

        public abstract Couleur getRadiance();

        public abstract Point getPosition();

        public abstract Vecteur getDirection(Point point);

        public abstract double Geo(Intersection info);
        public abstract double pdf();
        public abstract void Sample();

        public void setWorld(World world) { this.world = world; }

        public bool Shadow(Ray ray, Intersection info)
        {
            foreach (var item in world.GetFormes())
            {
                if (item is Accelarator) {

                    if (ShadowAccelerator(ray, info, (Accelarator)item)) return true;
                    continue;
                }

               
                if(ShadowForme(ray, info, item)) return true;


            }
            return false;
        }

        bool ShadowAccelerator(Ray ray, Intersection info, Accelarator accelarator) 
        { 
        
            foreach (var item in accelarator.GetForme())
            {
                if(ShadowForme(ray,info,item)) return true;
            }
            return false;
        }

        bool ShadowForme(Ray ray, Intersection info, Forme forme)
        {
            if (forme == info.objet) return false;

            if (this is AreaLight area && forme == area.forme) return false;
            
            if(!forme.boundingBox(ray)) return false;

            Intersection tmp = new Intersection(info.world, false);
            if (forme.Intersection(ray,ref tmp))
            {
                Vecteur toLight = getPosition() - ray.origine;
                if (tmp.t < toLight.norme) return true;
            }
            return false;
        }
    }
}
