using _2D_engine;


namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            World world = new World();
            Image img = new Image(world.RenderScene());
            img.saveImage("Outpout.png");
        }
    }
}