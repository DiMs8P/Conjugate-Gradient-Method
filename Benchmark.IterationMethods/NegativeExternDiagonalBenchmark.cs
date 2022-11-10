using Benchmark.IterationMethods.Setups;
using BenchmarkDotNet.Attributes;
using Conjugate_Gradient_Method.Calculus;
using GaussMethod.Logging;
using GaussMethod.Types;
using GaussMethod;
using Conjugate_Gradient_Method.Matrix;
using System;
using Benchmark.IterationMethods.Setups.SparceGenerators;

namespace Benchmark.IterationMethods
{
    public class NegativeExternDiagonalBenchmark
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

        public void GaussSetup()
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
        public void GradientSetup()
        {
            Sparse10X10Generator generator = new Sparse10X10Generator();
            _matrix = generator.Matrix;
            _matrix.L = InvertTriangle.Invert(_matrix.L);
            _matrix.U = InvertTriangle.Invert(_matrix.U);

            _factMatrix1 = new SparseMatrix(_matrix.Diag);

            double[] identity = _matrix.Diag.Select(x => 1.0).ToArray();
            _factMatrix2 = new SparseMatrix(identity);

            //_factMatrix3 = new SparseMatrix(_matrix.Diag);

            _gradientMethodParams = new MethodParams(30000, 0.000000000001);
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
