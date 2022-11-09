using Conjugate_Gradient_Method.IO;

namespace GaussMethod.IO
{
    public class MatrixReader : FileReader<double[][]>
    {
        private readonly string _fileName;

        public MatrixReader(string fileName, string rootPath) 
            : base(rootPath)
        {
            _fileName = fileName;
        }

        public override double[][] Read()
        {
            return ReadFromFile(_fileName, ReadingMethod);
        }

        private double[][] ReadingMethod(StreamReader stream)
        {
            var sizes = stream.ReadLine()
                .Split(' ')
                .Take(2)
                .Select(int.Parse)
                .ToArray();

            var (height, width) = (sizes[0], sizes[1]);

            var values = new double[height][];

            for (int i = 0; i < height; i++)
            {
                var line = stream.ReadLine()
                    .Split(' ')
                    .Select(ParseElement)
                    .ToArray();

                values[i] = line;
            }

            return values;
        }

        private double ParseElement(string elem) => double.Parse(elem);
    }
}
