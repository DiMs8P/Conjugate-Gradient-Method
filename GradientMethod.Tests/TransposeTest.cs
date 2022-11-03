using Conjugate_Gradient_Method.Calculus;

namespace GradientMethod.Tests
{
    internal class MultiplyTest
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

            _matrix = new(
                l: new SparseMatrixTriangle(values, ig, jg),
                u: new SparseMatrixTriangle(new double[228], new int[228], new int[228]),
                diag: new double[228]
            );
        }

        [TestCase(new double[] { 1, 2, 3, 4, 5, 6 }, new double[] { 25, 0, 24, 18, 0, 0 })]
        public void Multiply(double[] xVec, double[] expected)
        {
            var result = _matrix.Multiply(xVec);

            Assert.That(expected.SequenceEqual(result), Is.True);
        }

        [TestCase(new double[] { 1, 2, 3, 4, 5, 6 }, new double[] { 0, 0, 3, 4, 0, 24 })]
        public void Transpose(double[] xVec, double[] expected)
        {
            var result = _matrix.MultiplyTranspose(xVec);

            Assert.That(expected.SequenceEqual(result), Is.True);
        }

    }
}