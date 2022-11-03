using Conjugate_Gradient_Method.Calculus;
using Conjugate_Gradient_Method.IO;

namespace Conjugate_Gradient_Method
{
    internal class Program
    {
        static void Main(string[] args)
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

            MatrixReader reader = new MatrixReader(
                new SparseMatrixFilesProvider(
                    "di.txt",
                    "ggl.txt",
                    "igl.txt",
                    "jgl.txt",
                    "ggu.txt",
                    "igu.txt",
                    "jgu.txt"
                    ),
                "../../../Input/");

            SparseMatrix matrix1 = reader.Read();
            /*double[] diag = new double[]
            {
                100,
                100,
                100,
                100,
                100,
                100,
            };*/

            /*SparseMatrix matrix = new(
                l: new SparseMatrixTriangle(new double[228], new int[228], new int[228]),
                u: new SparseMatrixTriangle(values, ig, jg),
                diag: diag
            );*/

            double[] f = {107, 100, 104, 103,100,100};

            MethodParams methodParams = new MethodParams(100000, 0.00000001);
            double[] result = Solution.CalcX(matrix1,
                new double[] { 0, 0, 0, 0, 0, 0 },
                f,
                new(
                    l: new SparseMatrixTriangle(new double[228], new int[228], new int[228]),
                    u: new SparseMatrixTriangle(new double[228], new int[228], new int[228]),
                    diag: new double[] { 1, 1, 1, 1, 1, 1 }),
                methodParams


            );
        }
    }
}
