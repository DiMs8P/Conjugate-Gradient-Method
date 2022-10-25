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
    }
}
