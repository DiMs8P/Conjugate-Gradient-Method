using Conjugate_Gradient_Method.Calculus;
using Conjugate_Gradient_Method.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math = Conjugate_Gradient_Method.Calculus.Math;

namespace GradientMethod.Tests
{
    internal class SlaeSolver
    {
        private SparseMatrix _matrix;
        private double[] diag;

        [SetUp]
        public void Setup()
        {
            //LOVER TRIANGLE

            int[] lig = { 0, 0, 0, 1, 2, 3 };
            int[] ljg =
            {
                1,2,3
            };

            double[] lvalues =
            {
                5, 10, -3
            };

            SparseMatrixTriangle _ltriangle = new SparseMatrixTriangle(lvalues, lig, ljg);

            //UPPER TRIANGLE

            int[] uig =
            {
                2,
                2,
                3,
                4,
                4,
                4
            };
            int[] ujg =
            {
                2, 3,
                5,
                5
            };

            double[] uvalues =
            {
                3, 4,
                4,
                3
            };

            SparseMatrixTriangle _utriangle = new SparseMatrixTriangle(uvalues, uig, ujg);

            diag = new double[]
            {
                100,
                100,
                100,
                100,
                100,
                100,
            };

            _matrix = new SparseMatrix(_ltriangle,
                _utriangle,
                diag
            );
        }

        [TestCase(new double[] { 107, 100, 104, 108, 110, 97 }, new double[] { 1, 1, 1, 1, 1, 1 })]
        public void Solve(double[] f, double[] expected)
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
