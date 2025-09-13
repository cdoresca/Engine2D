using System;
using System.Xml;
using _2D_engine;


namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            World world = new World();
            world.build();
            Image img = new Image(world.renderScene());
            img.saveImage("Output.png");
        }
    }
}