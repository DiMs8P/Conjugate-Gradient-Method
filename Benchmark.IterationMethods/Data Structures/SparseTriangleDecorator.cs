using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conjugate_Gradient_Method.Matrix;

namespace Conjugate_Gradient_Method.Data_Structures.Matrix
{
    public class SparseTriangleDecorator : SparseMatrixTriangle
    {
        public SparseTriangleDecorator(double[] values, int[] rowPtr, int[] columnPtr) : base(values, rowPtr, columnPtr)
        {
        }

        public SparseTriangleDecorator() : base(new double[]{}, new int[]{}, new int[]{})
        {
        }

        public override void MultiplyTranspose(double[] vector, double[] result)
        {
            if (!RowPtr.IsEmpty)
            {
                base.MultiplyTranspose(vector, result);
            }
        }
        public override void Multiply(double[] vector, double[] result)
        {
            if (!RowPtr.IsEmpty)
            {
                base.Multiply(vector, result);
            }
        }
    }
}
