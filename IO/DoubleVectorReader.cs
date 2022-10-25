namespace Conjugate_Gradient_Method.IO
{
    public class DoubleVectorReader : VectorReader<double>
    {
        public DoubleVectorReader(string fileName, string rootPath)
            : base(fileName, rootPath)
            { }

        protected override double ParseElement(string elem) => double.Parse(elem);
    }
}
