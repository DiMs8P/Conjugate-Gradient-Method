using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Conjugate_Gradient_Method.Calculus;
using Conjugate_Gradient_Method.Data_Structures.Matrix;
using Conjugate_Gradient_Method.IO;
using Conjugate_Gradient_Method.Matrix;

namespace Benchmark.IterationMethods.Methods
{
    public class SMGDifferentFactorizatonMatrixBenchmark
    {
        private SparseMatrix _matrix;
        private double[] _initialX;
        private SparseMatrix _factMatrix1;
        private SparseMatrix _factMatrix2;
        private SparseMatrix _factMatrix3;
        public SMGDifferentFactorizatonMatrixBenchmark()
        {
            _factMatrix1 = new SparseMatrixDecorator(_matrix.Diag); ;

            double[] identity = _matrix.Diag.Select(x => 1.0).ToArray();
            _factMatrix2 = new SparseMatrixDecorator(identity);

            _factMatrix3 = new SparseMatrixDecorator(_matrix.Diag);

            _initialX = new double[_matrix.Diag.Length];
        }

        [Benchmark]
        public void IdentityMatrix()
        {
            SGM.CalcX(_matrix, _initialX, )
        }
    }
}
