using System;
using System.Numerics;
using Conjugate_Gradient_Method.Matrix;

namespace Conjugate_Gradient_Method.Calculus
{
    public static class Sole
    {
        public static void LowerTriangleInverseMethod(SparseMatrixTriangle triangle, double[] diag, double[] result, double[] f)
        {
            Array.Clear(result, 0, result.Length);

            for (var i = 0; i < result.Length; i++)
            {
                foreach (var (columnIndex, value) in triangle.ColumnIndexValuesByRow(i))
                {
                    result[i] += value * result[columnIndex];
                }
                result[i] = (f[i] - result[i]) / diag[i];
            }
        }

        public static void UpperTriangleInverseMethod(SparseMatrixTriangle triangle, double[] diag, double[] result, double[] f)
        {
            Array.Clear(result, 0, result.Length);

            for (var i = result.Length - 1; i >= 0; i--)
            {
                foreach (var (columnIndex, value) in triangle.ColumnIndexValuesByRow(i))
                {
                    result[i] += value * result[columnIndex];
                }
                result[i] = (f[i] - result[i]) / diag[i];
            }
        }

        public static void TransposeLowerTriangleInverseMethod(SparseMatrixTriangle triangle, double[] diag, double[] result, double[] f)
        {
            Array.Clear(result, 0, result.Length);

            for (var i = result.Length - 1; i >= 0; i--)
            {
                var elem = f[i] / diag[i];
                result[i] = elem;

                foreach (var (columnIndex, value) in triangle.ColumnIndexValuesByRow(i))
                {
                    f[columnIndex] -= value * elem;
                }
            }
        }

        public static void TransposeUpperTriangleInverseMethod(SparseMatrixTriangle triangle, double[] diag, double[] result, double[] f)
        {
            Array.Clear(result, 0, result.Length);

            for (var i = 0; i < result.Length; i++)
            {
                var elem = f[i] / diag[i];
                result[i] = elem;

                foreach (var (columnIndex, value) in triangle.ColumnIndexValuesByRow(i))
                {
                    f[columnIndex] -= value * elem;
                }
            }
        }


    }
}
