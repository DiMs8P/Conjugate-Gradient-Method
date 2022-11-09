using Conjugate_Gradient_Method.Calculus;
using Conjugate_Gradient_Method.Matrix;
using Math = Conjugate_Gradient_Method.Calculus.Math;

namespace GradientMethod.Tests
{
    internal class SlaeLoverSolver
    {
        private SparseMatrix _matrix;
        private double[] diag;

        [SetUp]
        public void Setup()
        {
            int[] ig = { 0, 0, 0, 1, 2, 3};
            int[] jg =
            {
                1,2,3
            };

            double[] values =
            {
                5, 10, -3
            };

            SparseMatrixTriangle _triangle = new SparseMatrixTriangle(values, ig, jg);

            diag = new double[]
            {
                100,
                100,
                100,
                100,
                100,
                100,
            };

            _matrix = new SparseMatrix(_triangle,
                new SparseMatrixTriangle(
            new double[228],
            new int[228],
            new int[228]),
                diag
                );
        }

        [TestCase(new double[] { 100, 100, 100, 105, 110, 97 }, new double[] { 1, 1, 1, 1, 1, 1 })]
        public void LoverTriangleSolve(double[] f, double[] expected)
        {

            var result = new double[f.Length];
            double minDiscrepancy = 0.0000001;
            MethodParams methodParams = new MethodParams(100000, minDiscrepancy);
            result = SGM.CalcX(_matrix,
                new double[] { 0, 0, 0, 0, 0, 0 },
                f,
                new(
                    l: new SparseMatrixTriangle(new double[228], new int[228], new int[228]),
                    u: new SparseMatrixTriangle(new double[228], new int[228], new int[228]),
                    diag: new double[] { 1, 1, 1, 1, 1, 1 }),
                methodParams
            );

            double discrepancy = Math.Norm(SGM.CalcDiscrepancy(result, expected)) / Math.Norm(expected);

            Assert.Less(discrepancy, minDiscrepancy);
        }
    }
}