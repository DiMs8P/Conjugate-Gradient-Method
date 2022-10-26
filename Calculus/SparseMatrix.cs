namespace Conjugate_Gradient_Method.Calculus
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

        public static double[] Multiply(SparseMatrix matrix, double[] vector)
        {
            var result = new double[vector.Length];

            for (var i = 0; i < vector.Length; i++)
            {
                foreach (var (index, value) in matrix.L.ColumnIndexValuesByRow(i))
                {
                    result[i] += value * vector[index];
                }

                result[i] += matrix.Diag[i] * vector[i];

                foreach (var (index, value) in matrix.U.ColumnIndexValuesByRow(i))
                {
                    result[i] += value * vector[index];
                }
            }

            return result;
        }
    }
}
