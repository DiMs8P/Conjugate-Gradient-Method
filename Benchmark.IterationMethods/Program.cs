using Benchmark.IterationMethods.Setups.SparceGenerators;
using BenchmarkDotNet.Running;
using Conjugate_Gradient_Method.Matrix;

namespace Benchmark.IterationMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<GilbertBenchmark>();
        }
    }
}