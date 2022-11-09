using Conjugate_Gradient_Method.Calculus;
using Conjugate_Gradient_Method.IO;
using Conjugate_Gradient_Method.Matrix;

namespace Conjugate_Gradient_Method
{
    internal class Program
    {
        static void Main(string[] args)
        {
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

            double[] f = {107, 100, 104, 108,110,97};

            ParamsReader paramsReader = new ParamsReader("kuslau.txt", "../../../Input/");
            paramsReader.Read();

            MethodParams methodParams = paramsReader.Read();

            var result = SGM.CalcX(matrix1,
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
