using Benchmark.IterationMethods.Setups;
using BenchmarkDotNet.Attributes;
using GaussMethod.Logging;
using GaussMethod.Types;
using GaussMethod;

namespace Benchmark.IterationMethods
{
    internal class NegativeExternDiagonalBenchmark
    {
        private DiagMatrix DiagMatrix;
        private double[] F;
        private double[] StartX;
        private GaussMethodParams _gaussMethodParams;
        private IterationSolver GaussSolver;

        [GlobalSetup]
        public void Setup()
        {
            var gaussSetup = NegativeExternDiagonalElementsGenerator.GetGaussTest();
            DiagMatrix = gaussSetup.matrix;
            StartX = gaussSetup.x;
            F = gaussSetup.f;

            _gaussMethodParams = new GaussMethodParams(
                Accuracy: 0.000000000001,
                Relaxation: 1.025, // Is it optimal relaxation???
                MaxIteration: 30000
            );

            IIterationMethodLogger logger = new DisabledLogger();

            GaussSolver = new IterationSolver(logger);
        }

        [Benchmark]
        public double[] Gauss() =>
            GaussSolver.GaussMethod(DiagMatrix, StartX, F, _gaussMethodParams);

        [Benchmark]
        public void Gradient()
        {

        }
    }
}
