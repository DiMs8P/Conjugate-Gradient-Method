namespace Conjugate_Gradient_Method.IO
{
    public abstract class FileReader<T>
    {
        protected readonly string RootPath;

        protected FileReader(string rootPath)
        {
            RootPath = rootPath;
        }

        public abstract T Read();

        protected TResult ReadFromFile<TResult>(
            string fileName, 
            Func<StreamReader, TResult> readingMethod
            )
        {
            using StreamReader stream = new (RootPath + fileName);

            return readingMethod(stream);
        }
    }
}
