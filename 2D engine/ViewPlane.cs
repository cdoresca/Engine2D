using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_engine
{
    internal class ViewPlane
    {
        public int height{get; set;}
        public int width{get; set;}
        public double pixelSize{get; set;}
        public double gamma{get; set;}
        public double invGamma{get; set;}
        public ViewPlane() { }
    }
}
