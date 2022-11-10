using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Benchmark.IterationMethods.Setups.SparceGenerators;
using BenchmarkDotNet.Attributes;
using Conjugate_Gradient_Method.Calculus;
using Conjugate_Gradient_Method.IO;
using Conjugate_Gradient_Method.Matrix;

namespace Benchmark.IterationMethods
{
    public class SMGDifferentFactorizatonMatrixBenchmark
    {
        private SparseMatrix _matrix;
        private double[] _initialX;
        private SparseMatrix _factMatrix1;
        private SparseMatrix _factMatrix2;
        private SparseMatrix _factMatrix3;
        private double[] f;
        private MethodParams _params;
        public SMGDifferentFactorizatonMatrixBenchmark()
        {
            Sparse10X10Generator generator = new Sparse10X10Generator();
            _matrix = generator.Matrix;

            InitFactMatrix();

            _initialX = new double[_matrix.Diag.Length];

            InitF();

            _params = new MethodParams(30000, 0.000000000001);
        }

        private void InitFactMatrix()
        {
            _factMatrix1 = new SparseMatrix(_matrix.Diag);

            double[] identity = _matrix.Diag.Select(x => 1.0).ToArray();
            _factMatrix2 = new SparseMatrix(identity);

            _factMatrix3 = new SparseMatrix(_matrix.Diag);
        }
        private void InitF()
        {
            DoubleVectorReader reader = new DoubleVectorReader("pr.txt", "C:\\Users\\Dima\\Desktop\\Input\\SparseInput\\");
            f = reader.Read().ToArray();
        }


        [Benchmark]
        public double[] IdentityMatrix()
        {
            return SGM.CalcX(_matrix, _initialX, f, _factMatrix2, _params);
        }

        [Benchmark]
        public double[] DiagMatrix()
        {
            return SGM.CalcX(_matrix, _initialX, f, _factMatrix3, _params);
        }
    }
}
