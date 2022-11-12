using Conjugate_Gradient_Method.IO;
using Conjugate_Gradient_Method.Matrix;

namespace Benchmark.IterationMethods.Setups.SparceGenerators
{
    internal class Sparse10X10Generator
    {
        public SparseMatrix Matrix { get; }

        public Sparse10X10Generator()
        {
            SparseMatrixReader reader = new SparseMatrixReader(
                new SparseMatrixFilesProvider(
                    "di.txt",
                    "ggl.txt",
                    "igl.txt",
                    "jgl.txt",
                    "ggu.txt",
                    "igu.txt",
                    "jgu.txt"
                ),
                Program.RootPath);

            Matrix = reader.Read();
        }
    }
}
