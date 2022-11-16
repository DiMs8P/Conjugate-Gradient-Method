using Conjugate_Gradient_Method.Calculus;
using Conjugate_Gradient_Method.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GradientMethod.Tests.Dicomposition
{
    internal class LU
    {
        public SparseMatrix _matrix;

        [SetUp]
        public void Setup()
        {
            Sparse10X10Generator generator = new Sparse10X10Generator();
            _matrix = generator.Matrix;



        }

        [TestCase(new double[] { 9, 27, 6, 8, 9, 11 }, new double[] { 1, 1, 1, 1, 1, 1 })]
        public void UpperTriangle(double[] f, double[] expected)
        {
            var result = new double[diag.Length];
            Sole.UpperTriangleInverseMethod(_triangle, diag, result, f);

            Assert.That(expected.SequenceEqual(result), Is.True);
        }
    }
}
