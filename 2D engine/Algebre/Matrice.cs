using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace _2D_engine.Algebre
{
    internal class Matrice
    {
        double[,] transpose;
        bool isIdentity;
        double[,] data;

        public Matrice()
        {
            isIdentity = Identity();
            data = new double[4, 4];
            Transpose();
        }
        public Matrice(double[,] tab)
        {
            data = tab;
            isIdentity = Identity();
            Transpose();

        }

        public Matrice(Vecteur col1, Vecteur col2, Vecteur col3)
        {
            data = new double[4,4]
            {
                    { col1[0], col2[0], col3[0], 0 }, 
                    { col1[1], col2[1], col3[1], 0 },
                    { col1[2], col2[2], col3[2], 0 },
                    { 0,       0,       0,       1 }
            };
            isIdentity = Identity();

        }
        bool Identity()
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
            for( int i =0; i < transpose.GetLength(0); i++)
            {
                for( int j = 0;j < transpose.GetLength(1); j++) 
                {
                    transpose[i, j] = data[j, i];
                }
            }
        }

        public Matrice GetTranspose() { return new Matrice(transpose); }

        public Matrice Translation(Vecteur vec)
        {
            double[,] tab = { { 1, 0, 0, vec[0]},
                              { 0, 1, 0, vec[1]},
                              { 0, 0, 1, vec[2]},
                              { 0, 0, 0, 1}
            };

            return new Matrice(tab);
        }

        public Matrice Scale(double x, double y, double z)
        {
            double[,] tab = { { x, 0, 0, 0},
                              { 0, y, 0, 0},
                              { 0, 0, z, 0},
                              { 0, 0, 0, 1}
            };
            return new Matrice(tab);
        }

        public Matrice RotateX(double angle) 
        {
            double[,] tab = { { 1, 0, 0, 0},
                              { 0, Math.Cos(angle), -Math.Sin(angle), 0},
                              { 0, Math.Sin(angle), Math.Cos(angle), 0},
                              { 0, 0, 0, 1}
            };
            return new Matrice(tab);
        }
        public Matrice RotateY(double angle)
        {
            double[,] tab = { { Math.Cos(angle), 0, Math.Sin(angle), 0},
                              { 0, 1, 0, 0},
                              { -Math.Sin(angle), 0, Math.Cos(angle), 0},
                              { 0, 0, 0, 1}
            };
            return new Matrice(tab);
        }

        public Matrice RotateZ(double angle)
        {
            double[,] tab = { {  Math.Cos(angle), -Math.Sin(angle), 0, 0},
                              { Math.Sin(angle), Math.Cos(angle), 0, 0},
                              { 0, 0, 1, 0},
                              { 0, 0, 0, 1}
            };
            return new Matrice(tab);
        }

        public Matrice Rotate(double angle, Vecteur directeur) 
        {
            if (directeur * directeur == 0)
                throw new ArgumentException("Le vecteur directeur ne peut pas être nul.");

            Vecteur dir = directeur.normalization();
            Vecteur col1 = CalculRotate(new Vecteur(1,0,0),angle, dir);
            Vecteur col2 = CalculRotate(new Vecteur(0,1,0),angle, dir);
            Vecteur col3 = CalculRotate(new Vecteur(0,0,1),angle, dir);

            return new Matrice(col1, col2, col3);
        }

        Vecteur CalculRotate(Vecteur vec, double angle, Vecteur directeur) 
        {
            Vecteur v_d = (vec * directeur) / (directeur * directeur) * directeur;
            Vecteur v1 = vec - v_d;
            Vecteur v2 = v1 % directeur;

            return v_d + v1 * Math.Cos(angle) + v2 * Math.Sin(angle);
        }

    }
}
