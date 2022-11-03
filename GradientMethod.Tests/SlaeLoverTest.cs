using Conjugate_Gradient_Method.Calculus;

namespace GradientMethod.Tests
{
    internal class SlaeLoverSolver
    {
        private SparseMatrixTriangle _triangle;
        private double[] diag;

        [SetUp]
        public void Setup()
        {
            int[] ig = { 0, 0, 2, 3, 4, 5 };
            int[] jg =
            {
                0,1,2,0,2
            };

            double[] values =
            {
                1,2,3,4,5
            };

            _triangle = new SparseMatrixTriangle(values, ig, jg);

            diag = new double[]
            {
                1,
                2,
                3,
                4,
                5,
                6,
            };
        }

        [TestCase(new double[] { 1, 2, 6, 7, 9, 11 }, new double[] { 1, 1, 1, 1, 1, 1 })]
        public void LoverTriangle(double[] f, double[] expected)
        {
            var result = new double[diag.Length];
            Sole.LowerTriangleInverseMethod(_triangle, diag, result, f);

            Assert.That(expected.SequenceEqual(result), Is.True);
        }
        
        [TestCase(new double[] { 6, 4, 11, 4, 5, 6 }, new double[] { 1, 1, 1, 1, 1, 1 })]
        public void LowerTransposeTriangle(double[] f, double[] expected)
        {
            var result = new double[diag.Length];
            Sole.TransposeLowerTriangleInverseMethod(_triangle, diag, result, f);

            Assert.That(expected.SequenceEqual(result), Is.True);
        }

    }
}