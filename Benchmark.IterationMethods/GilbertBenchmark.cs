using Benchmark.IterationMethods.Setups;
using Benchmark.IterationMethods.Setups.SparceGenerators;
using BenchmarkDotNet.Attributes;
using Conjugate_Gradient_Method.Calculus;
using Conjugate_Gradient_Method.Matrix;
using GaussMethod;
using GaussMethod.Logging;
using GaussMethod.Types;

namespace Benchmark.IterationMethods
{
    public class GilbertBenchmark
    {
        private DiagMatrix DiagMatrix;
        private double[] F;
        private double[] StartX;
        private GaussMethodParams _gaussMethodParams;
        private IterationSolver GaussSolver;

        private SparseMatrix _matrix;
        private SparseMatrix _factMatrix1;
        private SparseMatrix _factMatrix2;
        private SparseMatrix _factMatrix3;
        private MethodParams _gradientMethodParams;

        public const int N = 5;

        [GlobalSetup]
        public void Setup()
        {
            GaussSetup();
            GradientSetup();
        }


        public void GaussSetup()
        {
            var gaussSetup = Gilbert5X5TestGenerator.GetGaussTest();
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
        public void GradientSetup()
        {
            _matrix = SparseHilbertGenerator.Generate(N);

            InitFactMatrix();

            _gradientMethodParams = new MethodParams(Program.MaxIteration, Program.Accuracy);
        }

        private void InitFactMatrix()
        {
            _factMatrix2 = new SparseMatrix(_matrix.Diag);

            double[] identity = _matrix.Diag.Select(x => 1.0).ToArray();
            _factMatrix1 = new SparseMatrix(identity);

            LUDecompositor decompositor = new LUDecompositor(_matrix);
            _factMatrix3 = decompositor.Decompose();
        }

        [Benchmark]
        public double[] Gauss() =>
            GaussSolver.GaussMethod(DiagMatrix, StartX, F, _gaussMethodParams);
        [Benchmark]
        public double[] Identity() =>
            SGM.CalcX(_matrix, StartX, F, _factMatrix1, _gradientMethodParams);
        [Benchmark]
        public double[] Diag() =>
            SGM.CalcX(_matrix, StartX, F, _factMatrix2, _gradientMethodParams);

        [Benchmark]
        public double[] LUDecomposition() =>
            SGM.CalcX(_matrix, StartX, F, _factMatrix3, _gradientMethodParams);
    }
}
