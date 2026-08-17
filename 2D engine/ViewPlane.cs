using _2D_engine.Sample;

namespace _2D_engine
{
    internal class ViewPlane
    {
        public int height { get; set; }
        public int width { get; set; }
        public double pixelSize { get; set; }
        public double gamma { get; set; }
        public double invGamma { get; set; }

        public int maxDepth { get; set; }
        public Sampler sampler { get; set; }
        public ViewPlane() { }
    }
}
