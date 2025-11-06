namespace _2D_engine.Algebre
{
    internal class GeomatricTransform
    {
        public Matrice matrix { get; set; }
        public Matrice inverse { get; set; }

        public GeomatricTransform(Matrice matrix, Matrice inverse)
        {
            this.matrix = matrix;
            this.inverse = inverse;
        }
        public GeomatricTransform()
        {
            matrix = new Matrice(); inverse = new Matrice();
        }

        public bool isIdentity() { return false; }

        public static GeomatricTransform Translation(Vecteur translation)
        {
            return new GeomatricTransform(Matrice.Translation(translation), Matrice.Translation(-1 * translation));
        }

        public static GeomatricTransform Scale(double x, double y, double z)
        {
            return new GeomatricTransform(Matrice.Scale(x, y, z), Matrice.Scale(1 / x, 1 / y, 1 / z));
        }

        public static GeomatricTransform RotationX(double angle)
        {
            return new GeomatricTransform(Matrice.RotateX(angle), Matrice.RotateX(angle).GetTranspose());
        }

        public static GeomatricTransform RotationY(double angle)
        {
            return new GeomatricTransform(Matrice.RotateY(angle), Matrice.RotateY(angle).GetTranspose());
        }
        public static GeomatricTransform RotationZ(double angle)
        {
            return new GeomatricTransform(Matrice.RotateZ(angle), Matrice.RotateZ(angle).GetTranspose());
        }

        public static GeomatricTransform Rotation(double angle, Vecteur dir)
        {
            return new GeomatricTransform(Matrice.Rotate(angle, dir), Matrice.Rotate(angle, dir).GetTranspose());
        }
        public static Ray TransformRay(Ray ray, Matrice mat)
        {
            return new Ray(mat * ray.origine, mat * ray.directeur);
        }
        public static Algebre.Point TransformPoint(Algebre.Point p, Matrice mat)
        {
            return new Algebre.Point(mat * p);
        }

        public static Vecteur TransformVecteur(Vecteur v, Matrice mat)
        {
            return new Vecteur(mat * v);
        }
        public static Normal TransformNormal(Normal v, Matrice mat)
        {
            return new Normal((mat * v).normalization());
        }
        public void Multiply(GeomatricTransform gt)
        {
            matrix = gt.matrix * matrix;
            inverse = inverse * gt.inverse;
        }
    }

}
