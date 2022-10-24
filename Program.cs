using System.Dynamic;
using System.Reflection;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Conjugate_Gradient_Method
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VectorIntFileReader intVectorReader = new VectorIntFileReader();
            VectorDoubleFileReader doubleVectorReader = new VectorDoubleFileReader();

            var x = intVectorReader.ReadVector("../../../input/di.txt");

        }
    }
    class SparseMatix
    {
        public int[] LRowPtr; 
        public int[] URowPtr;
        public int[] LCollumPrt;
        public int[] UCollumPrt;
        public double[] LData;
        public double[] UData;
        public double[] Diag;
    }


    abstract class VectorFileReader<T>
    {
        public abstract IEnumerable<T> ReadVector(string path);
    }
    class VectorIntFileReader : VectorFileReader<int>
    {
        public override IEnumerable<int> ReadVector(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string? text = reader.ReadLine();
                Console.WriteLine(text);
                return text?.Split(' ').Select(n => Convert.ToInt32(n));
            }
        }
    }
    class VectorDoubleFileReader : VectorFileReader<double>
    {
        public override IEnumerable<double> ReadVector(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string? text = reader.ReadLine();
                Console.WriteLine(text);
                return text?.Split(' ').Select(Convert.ToDouble);
            }
        }
    }

    class MatrixReader
    {
        public MatrixReader(string[] matrixDataPaths)
        {
            _path = matrixDataPaths;
        }

        public SparseMatix Read()
        {
            if (_path.Length > 6)
                return new SparseMatix();

            VectorIntFileReader intVectorReader = new VectorIntFileReader();
            VectorDoubleFileReader doubleVectorReader = new VectorDoubleFileReader();

            var x = intVectorReader.ReadVector("");

            SparseMatix matrix = new SparseMatix();


            return new SparseMatix
            {
                LowerTrianglePointer = intVectorReader.ReadVector(_path[1]).ToArray(),
                UpperTrianglePointer = intVectorReader.ReadVector(_path[2]).ToArray(),
                LowerTriangleData = doubleVectorReader.ReadVector(_path[3]).ToArray(),
                UpperTriangleData = doubleVectorReader.ReadVector(_path[4]).ToArray(),
                Diag = doubleVectorReader.ReadVector(_path[0]).ToArray()
            };
        }

        private string[] _path;
    }
}
