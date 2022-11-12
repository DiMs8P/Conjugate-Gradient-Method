using Conjugate_Gradient_Method.Matrix;

namespace Benchmark.IterationMethods.Setups
{
    internal class InvertTriangle
    {
        public static SparseMatrixTriangle Invert(SparseMatrixTriangle triangle)
        {
            return new SparseMatrixTriangle(
                triangle.Values
                .Select(x => -x)
                .ToArray(),
                
                triangle.RowPtr
                .ToArray(),
                
                triangle.ColumnPtr
                .ToArray()
                );
        }
    }
}
