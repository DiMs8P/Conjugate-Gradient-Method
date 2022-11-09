using GaussMethod.Types;

namespace Benchmark.IterationMethods.Setups
{
    internal class Gilbert5X5TestGenerator
    {
        public static (DiagMatrix matrix, double[] x, double[] f) GetGaussTest()
        {
            var diags = new double[][]
            {
                new double[] { 0, 0, 0, 0, 1/5d },
                new double[] { 0, 0, 0, 1/4d, 1/6d },
                new double[] { 0, 0, 1/3d, 1/5d, 1/7d },

                new double[] { 0, 1/2d, 1/4d, 1/6d, 1/8d },
                new double[] { 1d, 1/3d, 1/5d, 1/7d, 1/9d },
                new double[] { 1/2d, 1/4d, 1/6d, 1/8d, 0 },

                new double[] { 1/3d, 1/5d, 1/7d, 0, 0 },
                new double[] { 1/4d, 1/6d, 0, 0, 0 },
                new double[] { 1/5d, 0, 0, 0, 0},
            };
            var x = new double[5];
            var f = new double[5];

            for (var i = 0; i < 5; i++)
            for (var j = 0; j < 5; j++)
                f[i] += (j + 1d) / (i + j + 1d);

            return (new DiagMatrix(diags), x, f);
        }
    }
}
