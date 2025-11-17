using System;
using _2D_engine.Algebre;

namespace _2D_engine.Sample
{
    internal abstract class Sampler
    {

        private int currentSquare = 0;
        private int currentCircle = 0;
        private int currentHemisphere = 0;

        public Sampler()
        {


            sample = new List<Point>();

            circleSamples = new List<Point>();
            hemisphereSamples = new List<Point>();
        }

        protected List<Point> sample, circleSamples, hemisphereSamples;

        public abstract void GenerateSample();

        public void mapSquareToCircle()
        {

            double x, y, r, phi;

            foreach (Point p in sample)
            {
                x = p[0] * 2 - 1;
                y = p[1] * 2 - 1;



                if (x > y && x > -y)
                {
                    r = x;
                    phi = (Math.PI / 4) * (y / x);
                }
                else if (x < y && x > -y)
                {
                    r = y;
                    phi = (Math.PI / 4) * (2 - x / y);
                }
                else if (x < y && x < -y)
                {
                    r = -x;
                    phi = (Math.PI / 4) * (4 + y / x);
                }
                else
                {
                    r = -y;
                    phi = (Math.PI / 4) * (6 - x / y);
                }
                x = r * Math.Cos(phi);
                y = r * Math.Sin(phi);
                circleSamples.Add(new Point(x, y));
            }


        }

        public void mapSquareToHemisphere()
        {
            double phi, theta,x,y,z;
            double alpha = 1;
            foreach (Point p in sample) 
            {
                phi = 2 * Math.PI * p[0]; 
                theta = Math.Acos(Math.Pow(1 - p[1], 1.0 / (alpha + 1.0)));

                x = Math.Sin(theta) * Math.Cos(phi);
                y = Math.Sin(theta) * Math.Sin(phi);
                z = Math.Cos(theta);

                hemisphereSamples.Add(new Point(x, y, z));
            }
            
        }

        public Point sampleUnitSquare()
        {
            if (sample.Count == 0)
                GenerateSample();

            Point p = sample[currentSquare % sample.Count];
            currentSquare++;
            return p;
        }
        public Point sampleUnitCircle()
        {
            if (circleSamples.Count == 0)
                mapSquareToCircle();

            Point p = circleSamples[currentCircle % circleSamples.Count];
            currentCircle++;
            return p;
        }

        public Point sampleUnitHemisphere()
        {
            if (hemisphereSamples.Count == 0)
                mapSquareToHemisphere();

            Point p = hemisphereSamples[currentHemisphere % hemisphereSamples.Count];
            currentHemisphere++;
            return p;
        }
    }
}
