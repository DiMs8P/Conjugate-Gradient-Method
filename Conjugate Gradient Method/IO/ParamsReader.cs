using Conjugate_Gradient_Method.Calculus;

namespace Conjugate_Gradient_Method.IO
{
    public class ParamsReader : FileReader<MethodParams>
    {
        private readonly string _fileName;

        public ParamsReader(string fileName, string rootPath)
            : base(rootPath)
        {
            _fileName = fileName;
        }

        public override MethodParams Read()
        {
            return ReadFromFile(_fileName, ReadingMethod);
        }

        private MethodParams ReadingMethod(StreamReader stream)
        {
            var text = stream.ReadToEnd();
            var numbers = text.Split('\n');
            if (numbers.Length < 2) throw new IndexOutOfRangeException();

            return new MethodParams(int.Parse(numbers[0]), double.Parse(numbers[1]));
        }
    }
}
