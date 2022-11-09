namespace Conjugate_Gradient_Method.Matrix
{
    public class SparseMatrix
    {
        public SparseMatrixTriangle L;
        public SparseMatrixTriangle U;
        public double[] Diag { get; }

        public SparseMatrix(SparseMatrixTriangle l, SparseMatrixTriangle u, double[] diag)
        {
            L = l;
            U = u;
            Diag = diag;
        }

        public double[] Multiply(double[] vector)
        {
            var result = new double[vector.Length];

            L.Multiply(vector, result);
            U.Multiply(vector, result);
            for (var i = 0; i < vector.Length; i++)
            {
                result[i] += Diag[i] * vector[i];
            }

            return result;
        }

        public double[] MultiplyTranspose(double[] vector)
        {
            var result = new double[vector.Length];

            L.MultiplyTranspose(vector, result);
            U.MultiplyTranspose(vector, result);
            for (var i = 0; i < vector.Length; i++)
            {
                result[i] += Diag[i] * vector[i];
            }

            return result;
        }
    }
}
