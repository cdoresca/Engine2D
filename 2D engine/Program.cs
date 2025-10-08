using System;
using System.Xml;
using _2D_engine;
using _2D_engine.Figure;
using _2D_engine.Algebre;
using System.Drawing;


namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            World world = new World();
            world.Build();
            _2D_engine.Image img = new _2D_engine.Image(world.RenderScene());
            img.saveImage("Triangle.png");
        }
    }
}