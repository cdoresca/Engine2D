using System;
using _2D_engine;


namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            World world = new World();
            world.build();
            world.rendderScene();
        }
    }
}