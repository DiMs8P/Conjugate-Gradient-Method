using Conjugate_Gradient_Method.IO;
using GaussMethod.IO;
using GaussMethod.Types;

namespace Benchmark.IterationMethods.Setups
{
    internal class NegativeExternDiagonalElementsGenerator
    {
        private const string Root = "C:\\Users\\Dima\\Desktop\\Input\\";

        public static (DiagMatrix matrix, double[] x, double[] f) GetGaussTest()
        {
            DoubleVectorReader fReader = new("F.txt", Root);
            DoubleVectorReader xReader = new("x.txt", Root);
            MatrixReader matrixReader = new("A.txt", Root);

            const int diagsShift = 2;

            var f = fReader.Read().ToArray();
            var x = xReader.Read().ToArray();
            DiagMatrix A = new(matrixReader, diagsShift);

            return (A, x, f);
        }
    }
}
