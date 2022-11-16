using BenchmarkDotNet.Running;

namespace Benchmark.IterationMethods
{
    internal class Program
    {
        public const string GaussRootPath = DimaRootPath;
        public const string RootPath = DimaRootPath;
        private const string DimaRootPath = "C:\\Users\\Dima\\Desktop\\Input\\";
        private const string VitiaRootPath = "C:\\Users\\vitia\\OneDrive\\Рабочий стол\\Input\\";

        public const double Accuracy = 0.000000000001;
        public const int MaxIteration = 30000;

        static void Main(string[] args)
        {
            BenchmarkRunner.Run<NegativeExternDiagonalBenchmark>();
        }
    }
}