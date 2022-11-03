using Conjugate_Gradient_Method.Calculus;
using Newtonsoft.Json.Linq;
using Math = Conjugate_Gradient_Method.Calculus.Math;

namespace GradientMethod.Tests
{
    internal class FindXSolver
    {
        private SparseMatrix _matrix;

        [SetUp]
        public void Setup()
        {
            int[] ig = { 2, 2, 3, 4, 4, 4 };
            int[] jg =
            {
                2,3,
                5,
                5,
            };

            double[] values =
            {
                3, 4,
                4,
                3
            };

            double[] diag = new double[]
            {
                1,
                2,
                3,
                4,
                5,
                6,
            };

            _matrix = new(
                l: new SparseMatrixTriangle(new double[228], new int[228], new int[228]),
                u: new SparseMatrixTriangle(values, ig, jg),
                diag: diag
            );
        }

        [TestCase(new double[] { 8, 2, 7, 8, 5, 6 }, new double[] { 1, 1, 1, 1, 1, 1 })]
        public void Solve(double[] f, double[] expected)
        {
            var result = new double[f.Length];
            double minDiscrepancy = 0.1;
            MethodParams methodParams = new MethodParams(100000, minDiscrepancy);
            result = Solution.CalcX(_matrix,
                new double[] { 0, 0, 0, 0, 0, 0 },
                f,
                new(
                    l: new SparseMatrixTriangle(new double[228], new int[228], new int[228]),
                    u: new SparseMatrixTriangle(new double[228], new int[228], new int[228]),
                    diag: new double[] { 1, 1, 1, 1, 1, 1 }),
                methodParams

                );

            double discrepancy = Math.Norm(Solution.CalcDiscrepancy(result, expected)) / Math.Norm(expected);

            Assert.Less(discrepancy, minDiscrepancy);
        }

    }
}