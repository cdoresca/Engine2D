using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Trace;
using _2D_engine.Algebre;


namespace _2D_engine.Illumination
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

        public void setWorld(World world) { this.world = world; }
    }
}
