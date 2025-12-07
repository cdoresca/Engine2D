using _2D_engine.Algebre;

namespace _2D_engine.Sample
{
    internal class StratifiedSampler : Sampler
    {
        private Random random;


        public StratifiedSampler(int nbParPixel) : base()
        {
            random = new Random();
            this.nbSamples = nbParPixel;
            GenerateSample();
        }

        public override void GenerateSample()
        {
            double x, y;

            double step = 1 / nbSamples;
            sample.Clear();
            for (int i = 0; i < nbSamples; i++)
            {
                for (int j = 0; j < nbSamples; j++)
                {
                    x = (i + random.NextDouble()) * step;
                    y = (j + random.NextDouble()) * step;

                    sample.Add(new Point(x, y));

                }

            }

        }
    }
}
