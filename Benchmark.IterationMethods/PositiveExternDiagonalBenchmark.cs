using GaussMethod.Types;
using GaussMethod;
using Benchmark.IterationMethods.Setups;
using BenchmarkDotNet.Attributes;
using GaussMethod.Logging;
using Conjugate_Gradient_Method.Calculus;
using Conjugate_Gradient_Method.Matrix;
using Benchmark.IterationMethods.Setups.SparceGenerators;

namespace Benchmark.IterationMethods
{
    public class PositiveExternDiagonalBenchmark
    {
        private DiagMatrix DiagMatrix;
        private double[] F;
        private double[] StartX;
        private GaussMethodParams _gaussMethodParams;
        private IterationSolver GaussSolver;

        private SparseMatrix _matrix;
        private SparseMatrix _factMatrix1;
        private SparseMatrix _factMatrix2;
        //private SparseMatrix _factMatrix3;
        private MethodParams _gradientMethodParams;

        [GlobalSetup]
        public void Setup()
        {
            GaussSetup();
            GradientSetup();
        }
        public void GradientSetup()
        {
            Sparse10X10Generator generator = new Sparse10X10Generator();
            _matrix = generator.Matrix;

            _factMatrix1 = new SparseMatrix(_matrix.Diag);

            double[] identity = _matrix.Diag.Select(x => 1.0).ToArray();
            _factMatrix2 = new SparseMatrix(identity);

            //_factMatrix3 = new SparseMatrix(_matrix.Diag);

            _gradientMethodParams = new MethodParams(Program.MaxIteration, Program.Accuracy);
        }

        public void GaussSetup()
        {
            var gaussSetup = PositiveExternDiagonalElementsGenerator.GetGaussTest();
            DiagMatrix = gaussSetup.matrix;
            StartX = gaussSetup.x;
            F = gaussSetup.f;

            _gaussMethodParams = new GaussMethodParams(
                Accuracy: Program.Accuracy,
                Relaxation: 1.025, // Is it optimal relaxation???
                MaxIteration: Program.MaxIteration
            );

            IIterationMethodLogger logger = new DisabledLogger();

            GaussSolver = new IterationSolver(logger);
        }

        [Benchmark]
        public double[] Gauss() =>
            GaussSolver.GaussMethod(DiagMatrix, StartX, F, _gaussMethodParams);

        [Benchmark]
        public double[] GradientIdentity() =>
             SGM.CalcX(_matrix, StartX, F, _factMatrix1, _gradientMethodParams);

        [Benchmark]
        public double[] GradientDiag() =>
            SGM.CalcX(_matrix, StartX, F, _factMatrix2, _gradientMethodParams);

        /*[Benchmark]
        public void GradientLU()
        {
            return SGM.CalcX(_matrix, StartX, F, _factMatrix2, new MethodParams());
        }*/
    }
}
