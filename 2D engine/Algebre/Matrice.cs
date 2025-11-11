namespace _2D_engine.Algebre
{
    internal class Matrice
    {
        double[,] transpose;
        bool isIdentity;
        double[,] data;

        public Matrice()
        {
            data = new double[4, 4];
            for (int i = 0; i < 4; i++)
                data[i, i] = 1.0;

            isIdentity = true;
            Transpose();
        }
        public Matrice(double[,] tab)
        {
            if (tab.GetLength(0) != 4 || tab.GetLength(1) != 4)
                throw new ArgumentException("Matrice doit être 4x4.");

            data = tab;
            isIdentity = IsIdentity();
            Transpose();

        }

        public Matrice(Vecteur col1, Vecteur col2, Vecteur col3)
        {
            data = new double[4, 4]
            {
                    { col1[0], col2[0], col3[0], 0 },
                    { col1[1], col2[1], col3[1], 0 },
                    { col1[2], col2[2], col3[2], 0 },
                    { 0,       0,       0,       1 }
            };
            isIdentity = IsIdentity();
            Transpose();
        }
        bool IsIdentity()
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    if (i == j)
                    {
                        if (data[i, j] != 1) { return false; }
                    }
                    if (i != j)
                    {
                        if (data[i, j] != 0) { return false; }
                    }
                }
            }
            return true;
        }

        void Transpose()
        {
            transpose = new double[4, 4];
            for (int i = 0; i < transpose.GetLength(0); i++)
            {
                for (int j = 0; j < transpose.GetLength(1); j++)
                {
                    transpose[i, j] = data[j, i];
                }
            }
        }

        public Matrice GetTranspose() { return new Matrice(transpose); }

        public static Matrice Translation(Vecteur vec)
        {
            double[,] tab = { { 1, 0, 0, vec[0]},
                              { 0, 1, 0, vec[1]},
                              { 0, 0, 1, vec[2]},
                              { 0, 0, 0, 1}
            };

            return new Matrice(tab);
        }

        public static Matrice Scale(double x, double y, double z)
        {
            double[,] tab = { { x, 0, 0, 0},
                              { 0, y, 0, 0},
                              { 0, 0, z, 0},
                              { 0, 0, 0, 1}
            };
            return new Matrice(tab);
        }

        public static Matrice RotateX(double angle)
        {
            double rad = angle  * Math.PI / 180.0;
            double[,] tab = { { 1, 0, 0, 0},
                              { 0, Math.Cos(rad), -Math.Sin(rad), 0},
                              { 0, Math.Sin(rad), Math.Cos(rad), 0},
                              { 0, 0, 0, 1}
            };
            return new Matrice(tab);
        }
        public static Matrice RotateY(double angle)
        {
            double rad = angle  * Math.PI / 180.0;
            double[,] tab = { { Math.Cos(rad), 0, Math.Sin(rad), 0},
                              { 0, 1, 0, 0},
                              { -Math.Sin(rad), 0, Math.Cos(rad), 0},
                              { 0, 0, 0, 1}
            };
            return new Matrice(tab);
        }

        public static Matrice RotateZ(double angle)
        {
            double rad = angle * Math.PI / 180.0;
            double[,] tab = { {  Math.Cos(rad), -Math.Sin(rad), 0, 0},
                              { Math.Sin(rad), Math.Cos(rad), 0, 0},
                              { 0, 0, 1, 0},
                              { 0, 0, 0, 1}
            };
            return new Matrice(tab);
        }

        public static Matrice Rotate(double angle, Vecteur directeur)
        {
            if (directeur * directeur == 0)
                throw new ArgumentException("Le vecteur directeur ne peut pas être nul.");
            double rad = angle * Math.PI / 180.0;
            Vecteur dir = directeur.normalization();
            Vecteur col1 = CalculRotate(new Vecteur(1, 0, 0), rad, dir);
            Vecteur col2 = CalculRotate(new Vecteur(0, 1, 0), rad, dir);
            Vecteur col3 = CalculRotate(new Vecteur(0, 0, 1), rad, dir);

            return new Matrice(col1, col2, col3);
        }

        public static Vecteur CalculRotate(Vecteur vec, double angle, Vecteur directeur)
        {
            Vecteur v_d = (vec * directeur) / (directeur * directeur) * directeur;
            Vecteur v1 = vec - v_d;
            Vecteur v2 = v1 % directeur;

            return v_d + v1 * Math.Cos(angle) + v2 * Math.Sin(angle);
        }

        public static Vecteur operator *(Matrice a, Vecteur v)
        {
            double x = a[0, 0] * v[0] + a[0, 1] * v[1] + a[0, 2] * v[2];
            double y = a[1, 0] * v[0] + a[1, 1] * v[1] + a[1, 2] * v[2];
            double z = a[2, 0] * v[0] + a[2, 1] * v[1] + a[2, 2] * v[2];

            return new Vecteur(x, y, z);
        }


        public static Point operator *(Matrice a, Algebre.Point b)
        {
            double x = a[0, 0] * b[0] + a[0, 1] * b[1] + a[0, 2] * b[2] + a[0, 3] * 1;
            double y = a[1, 0] * b[0] + a[1, 1] * b[1] + a[1, 2] * b[2] + a[1, 3] * 1;
            double z = a[2, 0] * b[0] + a[2, 1] * b[1] + a[2, 2] * b[2] + a[2, 3] * 1;
            double w = a[3, 0] * b[0] + a[3, 1] * b[1] + a[3, 2] * b[2] + a[3, 3] * 1;

            if (w != 0 && w != 1)
            {
                x /= w;
                y /= w;
                z /= w;
            }

            return new Point(x, y, z);
        }

        public double this[int i, int j]
        {
            get
            {
                return data[i, j];

            }
            set
            {
                data[i, j] = value;
            }
        }


        public static Matrice operator *(Matrice a, Matrice b)
        {
            var result = new Matrice();

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    result[row, col] = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        result[row, col] += a[row, k] * b[k, col];
                    }
                }
            }

            return result;
        }
    }
}
