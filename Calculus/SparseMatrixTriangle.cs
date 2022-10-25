namespace Conjugate_Gradient_Method.Calculus
{
    public class SparseMatrixTriangle
    {
        public double[] Values { get; }
        public ReadOnlySpan<int> RowPtr => new(_rowPtr);
        public ReadOnlySpan<int> ColumnPtr => new(_columnPtr);

        private readonly int[] _rowPtr;
        private readonly int[] _columnPtr;

        public SparseMatrixTriangle(double[] values, int[] rowPtr, int[] columnPtr)
        {
            Values = values;
            _rowPtr = rowPtr;
            _columnPtr = columnPtr;
        }
    }
}
