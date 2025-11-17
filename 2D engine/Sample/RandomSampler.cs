using System;
using _2D_engine.Algebre;


namespace _2D_engine.Sample
{
    internal class RandomSampler : Sampler
    {
       
        Random random;
        int nbSample;

        public RandomSampler(int nbSample) : base()
        { 
            random = new Random();
            this.nbSample = nbSample;

        }
        public override void GenerateSample()
        {
            double x, y;
            for (int i = 0; i < nbSample; i++)
            {
                x = random.NextDouble();
                y = random.NextDouble();

                sample.Add(new Point(x,y));
                
            }
           
        }
    }
}
