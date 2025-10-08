using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_engine.Algebre
{
    internal class Matrice3x3
    {
        double[,] transpose;
        bool isIdentity;
        double[,] data;

        public Matrice3x3()
        {
            data = new double[3, 3];
            for (int i = 0; i < 3; i++)
                data[i, i] = 1.0;

            isIdentity = true;
            Transpose();
        }
        public Matrice3x3(double[,] tab)
        {
            if (tab.GetLength(0) != 3 || tab.GetLength(1) != 3)
                throw new ArgumentException("Matrice doit être 3x3.");

            data = tab;
            isIdentity = IsIdentity();
            Transpose();

        }

        public Matrice3x3(Vecteur col1, Vecteur col2, Vecteur col3)
        {
            data = new double[3, 3]
            {
                    { col1[0], col2[0], col3[0]},
                    { col1[1], col2[1], col3[1]},
                    { col1[2], col2[2], col3[2] }
                    
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
            transpose = new double[3, 3];
            for (int i = 0; i < transpose.GetLength(0); i++)
            {
                for (int j = 0; j < transpose.GetLength(1); j++)
                {
                    transpose[i, j] = data[j, i];
                }
            }
        }

        public Matrice GetTranspose() { return new Matrice(transpose); }

        public double Determinant()
        {
            return data[0, 0] * (data[1, 1] * data[2, 2] - data[1, 2] * data[2, 1])
                 - data[0, 1] * (data[1, 0] * data[2, 2] - data[1, 2] * data[2, 0])
                 + data[0, 2] * (data[1, 0] * data[2, 1] - data[1, 1] * data[2, 0]);
        }

        public Vecteur getColonne(int pos)
        {
            Vecteur v = new Vecteur();

            for (int i = 0; i < data.GetLength(0); i++)
            {
                v[i] = data[i, pos];
            }
            return v;
        }
    }
}
