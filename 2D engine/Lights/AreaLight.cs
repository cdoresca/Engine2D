using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Algebre;
using _2D_engine.Figure;
using _2D_engine.Illumination;
using _2D_engine.Materiel;
using _2D_engine.Trace;

namespace _2D_engine.Lights
{
    internal class AreaLight : Light
    {
        public Forme forme;
        Material material;
        
        public AreaLight(Forme f, Material m) 
        {
            forme = f;
            material = m;
        }

        public override double Geo(Intersection info) 
        { 
            
            Point pointObject = GeomatricTransform.TransformPoint(position, forme.transform.inverse);
            Normal normal = forme.GetNormal(pointObject);
            Normal normalWorld = GeomatricTransform.TransformNormal(normal, forme.transform.inverse.GetTranspose()).normalization();
            Vecteur w = position - info.point;

            return Math.Abs(normalWorld * (-1 * getDirection(info.point))) / (w.norme * w.norme);
        }
        public override double pdf() { return forme.pdf(); }

        public override Vecteur getDirection(Point point)
        {
            return (position - point).normalization();
        }

        public override Point getPosition()
        {
            return position;
        }

        public override Couleur getRadiance()
        {
            return material.GetRadiance();
        }

        public override void Sample() { position = forme.Sample(); }

    }
}
