using Conjugate_Gradient_Method.Calculus;

namespace Conjugate_Gradient_Method.IO
{
    public class SparseMatrixReader : FileReader<SparseMatrix>
    {
        private readonly SparseMatrixFilesProvider _paths;

        public SparseMatrixReader(SparseMatrixFilesProvider paths, string rootPath)
            : base(rootPath)
        {
            _paths = paths;
        }

        public override SparseMatrix Read()
        {
            return new SparseMatrix(
                l: new SparseMatrixTriangle(
                    ReadValuesVector(_paths.GGL).ToArray(),
                    ReadIndexesVector(_paths.IGL).ToArray(),
                    ReadIndexesVector(_paths.JGL).ToArray()
                ),
                u: new SparseMatrixTriangle(
                    ReadValuesVector(_paths.GGU).ToArray(),
                    ReadIndexesVector(_paths.IGU).ToArray(),
                    ReadIndexesVector(_paths.JGU).ToArray()
                ),
                diag: ReadValuesVector(_paths.DI).ToArray()
                );
        }

        private IEnumerable<double> ReadValuesVector(string path)
        {
            var reader = new DoubleVectorReader(path, RootPath);
            return reader.Read();
        }

        private IEnumerable<int> ReadIndexesVector(string path)
        {
            var reader = new IntVectorReader(path, RootPath);
            return reader.Read();
        }
    }
}
