using BenchmarkDotNet.Running;

namespace Benchmark.IterationMethods
{
    internal class Program
    {
        public const string GaussRootPath = "C:\\Users\\vitia\\OneDrive\\Рабочий стол\\Input\\";
        public const string RootPath = "C:\\Users\\vitia\\OneDrive\\Рабочий стол\\Input\\SparseInput\\";
        public const double Accuracy = 0.000000000001;
        public const int MaxIteration = 30000;

        static void Main(string[] args)
        {
            //NegativeExternDiagonalBenchmark benchmark = new ();
            //benchmark.GaussSetup();
            //benchmark.Gauss();
            BenchmarkRunner.Run<NegativeExternDiagonalBenchmark>();
        }
    }
}