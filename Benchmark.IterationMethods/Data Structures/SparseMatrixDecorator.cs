using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conjugate_Gradient_Method.Matrix;

namespace Conjugate_Gradient_Method.Data_Structures.Matrix
{
    public class SparseMatrixDecorator : SparseMatrix
    {
        private readonly SparseMatrix _matrix;
        public SparseMatrixDecorator(SparseMatrixTriangle l, SparseMatrixTriangle u, double[] diag) : base(l, u, diag) { }

        public SparseMatrixDecorator(double[] diag) : base(new SparseTriangleDecorator(), new SparseTriangleDecorator(), diag)
        {
        }

        public double[] Diag() => _matrix.Diag;

    }
}
