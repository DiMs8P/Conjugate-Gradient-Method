namespace Conjugate_Gradient_Method.Calculus
{
    public class SparseMatrixTriangle
    {
        public IEnumerable<IndexValue> ColumnIndexValuesByRow(int rowIndex)
        {
            if (rowIndex < 0) throw new ArgumentOutOfRangeException(nameof(rowIndex));

            var end = _rowPtr[rowIndex];

            if (end < 0)
                yield break;
            //var begin = end == 0 ? 0 : _rowPtr[rowIndex - 1] + 1;
            
            var begin = rowIndex == 0 || end == 0
                ? 0
                : _rowPtr[rowIndex - 1] + 1;


            for (int i = begin; i <= end; i++)
                yield return new IndexValue(_columnPtr[i], Values[i]);
        }

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
