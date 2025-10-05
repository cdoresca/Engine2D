using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _2D_engine.Algebre
{
    internal class GeomatricTransform
    {
        Matrice matrix;
        Matrice inverse;

        GeomatricTransform(Matrice matrix, Matrice inverse)
        {
            this.matrix = matrix;
            this.inverse = inverse;
        }

        bool isIdentity() {  return false; }
        Matrice GetMatrix() { return matrix; }
        Matrice GetInverse() { return inverse; }

        GeomatricTransform Translation(Vecteur translation)
        {
            return new GeomatricTransform(matrix.Translation(translation), inverse.Translation(-1 * translation));
        }

        GeomatricTransform Scale(double x, double y, double z)
        {
            return new GeomatricTransform(matrix.Scale(x, y, z), inverse.Scale(1 / x, 1 / y, 1 / z));
        }

        GeomatricTransform RotationX(double angle) 
        { 
            return new GeomatricTransform(matrix.RotateX(angle), inverse.RotateX(angle).GetTranspose());
        }

        GeomatricTransform RotationY(double angle)
        {
            return new GeomatricTransform(matrix.RotateY(angle), inverse.RotateY(angle).GetTranspose());
        }
        GeomatricTransform RotationZ(double angle)
        {
            return new GeomatricTransform(matrix.RotateZ(angle), inverse.RotateZ(angle).GetTranspose());
        }

        GeomatricTransform Rotation(double angle,Vecteur dir)
        {
            return new GeomatricTransform(matrix.Rotate(angle,dir), inverse.Rotate(angle,dir).GetTranspose());
        }




    }

}
