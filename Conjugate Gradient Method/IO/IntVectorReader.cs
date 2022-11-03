namespace Conjugate_Gradient_Method.IO
{
    public class IntVectorReader : VectorReader<int>
    {
        public IntVectorReader(string fileName, string rootPath)
            : base(fileName, rootPath)
            { }

        protected override int ParseElement(string elem) => int.Parse(elem);
    }
}
