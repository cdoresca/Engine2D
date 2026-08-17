using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Illumination;
using _2D_engine.Trace;

namespace _2D_engine.Materiel
{
    internal class IsotropicLight : Material
    {
        double ls;
        Couleur color;

        public IsotropicLight(double ls, Couleur color)
        {
            this.ls = ls;
            this.color = color;
        }

        public void SetColor(Couleur c) { this.color = c; } 

        public void SetScale(double s) {  this.ls = s; }

        public override Couleur shade(Intersection info)
        {
            return this.color * ls;
        }

        public override Couleur GetRadiance() {  return this.color * ls; }
    }
}
