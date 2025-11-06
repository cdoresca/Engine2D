namespace _2D_engine.Algebre
{
    internal class Normal : Vecteur
    {
        public Normal(double x = 0, double y = 0, double z = 0) : base(x, y, z)
        {

        }

        public Normal(Vecteur v1, Vecteur v2) : base(v1.cross(v2))
        {

        }

        public Normal(Vecteur v) : base(v)
        {
        }
    }
}
