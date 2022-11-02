﻿namespace Conjugate_Gradient_Method.Calculus
{
    public class SparseMatrixTriangle
    {
        public IEnumerable<IndexValue> ColumnIndexValuesByRow(int rowIndex)
        {
            if (rowIndex < 0) throw new ArgumentOutOfRangeException(nameof(rowIndex));

            var end = _rowPtr[rowIndex];

            var begin = rowIndex == 0
                ? 0
                : _rowPtr[rowIndex - 1];

            for (int i = begin; i < end; i++)
                yield return new IndexValue(_columnPtr[i], Values[i]);
        }

        public void Multiply(double[] vector, double[] result)
        {
            if (vector.Length != result.Length) throw new IndexOutOfRangeException(nameof(vector));

            for (var i = 0; i < vector.Length; i++)
            {
                foreach (var (columnIndex, value) in ColumnIndexValuesByRow(i))
                {
                    result[i] += value * vector[columnIndex];
                }
            }
        }

        public void MultiplyTranspose(double[] vector, double[] result)
        {
            if (vector.Length != result.Length) throw new IndexOutOfRangeException(nameof(vector));

            for (var i = 0; i < vector.Length; i++)
            {
                foreach (var (columnIndex, value) in ColumnIndexValuesByRow(i))
                {
                    result[columnIndex] += value * vector[i];
                }
            }
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
