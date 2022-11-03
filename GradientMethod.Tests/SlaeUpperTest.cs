
using Conjugate_Gradient_Method.Calculus;

namespace GradientMethod.Tests
{
    internal class SlaeUpperTest
    {
        private SparseMatrixTriangle _triangle;
        private double[] diag;

        [SetUp]
        public void Setup()
        {
            int[] ig =
            {
                4,
                7,
                8,
                9,
                9,
                9
            };
            int[] jg =
            {
                1,2,3,5,
                3,4,5,
                4,
                5
            };

            double[] values =
            {
                3, -2, 1, 6,
                6, 9, 10,
                3,
                4
            };

            _triangle = new SparseMatrixTriangle(values, ig, jg);

            diag = new double[]
            {
                1,
                2,
                3,
                4,
                9,
                11,
            };

        }

        [TestCase(new double[] {9, 27, 6, 8, 9, 11 }, new double[] { 1, 1, 1, 1, 1, 1 })]
        public void UpperTriangle(double[] f, double[] expected)
        {
            var result = new double[diag.Length];
            Sole.UpperTriangleInverseMethod(_triangle, diag, result, f);

            Assert.That(expected.SequenceEqual(result), Is.True);
        }

        [TestCase(new double[] { 1, 5, 1, 11, 21, 31 }, new double[] { 1, 1, 1, 1, 1, 1 })]
        public void UpperTransposeTriangle(double[] f, double[] expected)
        {
            var result = new double[diag.Length];
            Sole.TransposeUpperTriangleInverseMethod(_triangle, diag, result, f);

            Assert.That(expected.SequenceEqual(result), Is.True);
        }
    }
}
