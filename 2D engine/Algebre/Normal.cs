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
        
        public Normal normalization()
        {
            return new Normal(base.normalization());
        }

        public static Normal operator *(double a, Normal b)
        {
            return new Normal(a * b[0], a * b[1], a * b[2]);
        }

        public static Normal operator *(Normal b, double a)
        {
            return new Normal(a * b[0], a * b[1], a * b[2]);
        }
    }
}
