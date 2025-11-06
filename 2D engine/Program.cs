using _2D_engine;


namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            World world = new World();
            world.Build();
            _2D_engine.Image img = new _2D_engine.Image(world.RenderScene());
            img.saveImage("Cube.png");
        }
    }
}