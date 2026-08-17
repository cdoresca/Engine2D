using _2D_engine.Algebre;


namespace _2D_engine.Sample
{
    internal class RandomSampler : Sampler
    {

        Random random;

        public RandomSampler(int nbSample) : base()
        {
            random = new Random();
            this.nbSamples = nbSample;
            GenerateSample();
        }
        public override void GenerateSample()
        {
            sample.Clear();
            double x, y;
            for (int i = 0; i < nbSamples; i++)
            {
                x = random.NextDouble();
                y = random.NextDouble();

                sample.Add(new Point(x, y));

            }

        }

    }
}
