using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using _2D_engine.Algebre;

namespace _2D_engine.Sample
{
    internal class StratifiedSampler : Sampler
    {
        private Random random;
        int nbParPixel;

        public StratifiedSampler(int nbParPixel) : base()
        {
            random = new Random();
            this.nbParPixel = nbParPixel;

        }

        public override void GenerateSample()
        {
            double x, y;

            double step = 1 / nbParPixel;
            
            for(int i = 0; i < nbParPixel; i++)
            {
                for (int j = 0; j < nbParPixel; j++) 
                {
                    x = (i + random.NextDouble()) * step;
                    y = (j + random.NextDouble()) * step;
                        
                    sample.Add(new Point(x, y));
                    
                }
                    
            }
            
            
        }
    }
}
