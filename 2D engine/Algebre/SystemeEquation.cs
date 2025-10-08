using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_engine.Algebre
{
    internal class SystemeEquation
    {
        public SystemeEquation() { }

        public static Vecteur Cramer(Matrice3x3 mat,Vecteur vec)
        {
            double denom = mat.Determinant();
            Vecteur col1 = mat.getColonne(0);
            Vecteur col2 = mat.getColonne(1);
            Vecteur col3 = mat.getColonne(2);

            Matrice3x3 a = new Matrice3x3(vec,col2,col3);
            Matrice3x3 b = new Matrice3x3(col1, vec, col3);
            Matrice3x3 c = new Matrice3x3(col1, col2, vec);

            double x = a.Determinant() / denom;
            double y = b.Determinant() / denom;
            double z = c.Determinant() / denom;

            return new Vecteur(x,y,z);

        }
    }
}
