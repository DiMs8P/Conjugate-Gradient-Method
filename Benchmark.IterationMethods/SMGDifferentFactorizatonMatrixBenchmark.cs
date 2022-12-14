using Benchmark.IterationMethods.Setups;
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

            _params = new MethodParams(Program.MaxIteration, Program.Accuracy);
        }

        private void InitFactMatrix()
        {
            _factMatrix2 = new SparseMatrix(_matrix.Diag);

            double[] identity = _matrix.Diag.Select(x => 1.0).ToArray();
            _factMatrix1 = new SparseMatrix(identity);

            LUDecompositor decompositor = new LUDecompositor(_matrix);
            _factMatrix3 = decompositor.Decompose();
        }
        private void InitF()
        {
            DoubleVectorReader reader = new DoubleVectorReader("pr.txt", Program.RootPath);
            f = reader.Read().ToArray();
        }


        [Benchmark]
        public double[] Identity()
        {
            return SGM.CalcX(_matrix, _initialX, f, _factMatrix1, _params);
        }

        [Benchmark]
        public double[] Diag()
        {
            return SGM.CalcX(_matrix, _initialX, f, _factMatrix2, _params);
        }

        [Benchmark]
        public double[] LUDecomposition()
        {
            return SGM.CalcX(_matrix, _initialX, f, _factMatrix3, _params);
        }
    }
}
