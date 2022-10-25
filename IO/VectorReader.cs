namespace Conjugate_Gradient_Method.IO
{
    public abstract class VectorReader<T> : FileReader<IEnumerable<T>>
    {
        private readonly string _fileName;

        protected VectorReader(string fileName, string rootPath)
            : base(rootPath)
        {
            _fileName = fileName;
        }

        public override IEnumerable<T> Read()
        {
            return ReadFromFile(_fileName, ReadingMethod);
        }

        private IEnumerable<T> ReadingMethod(StreamReader stream)
        {
            var text = stream.ReadLine();

            return text.Split(' ').Select(ParseElement);
        }

        protected abstract T ParseElement(string elem);

        //public static IEnumerable<double> FromFile();
    }
}
