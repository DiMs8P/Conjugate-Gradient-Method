using GaussMethod.Logging;
using GaussMethod.Types;

namespace GaussMethod
{
    public class IterationSolver
    {
        private static readonly int[] IndexesMemory;

        private readonly IIterationMethodLogger _logger;

        static IterationSolver()
        {
            IndexesMemory = new int[DiagMatrix.DiagsCount];
        }

        public IterationSolver(IIterationMethodLogger logger)
        {
            _logger = logger;
        }

        public double[] GaussMethod(DiagMatrix matrix, double[] x, double[] f, GaussMethodParams parameters)
        {
            var fNorm = VectorMath.GetNorm(f);
            var discrepancyNorm = fNorm;
            var relativeDiscrepancy = discrepancyNorm / fNorm;

            var indexes = IndexesMemory;
            matrix.GetIndexes(indexes);

            int k;
            for (k = 0; (k < parameters.MaxIteration) && (relativeDiscrepancy > parameters.Accuracy); ++k)
            {
                _logger.Log(k, relativeDiscrepancy);
                
                Iterate(matrix, x, f, x, parameters, indexes, out discrepancyNorm);

                relativeDiscrepancy = discrepancyNorm / fNorm;
            }

            _logger.LogFinish(k, relativeDiscrepancy);

            return x;
        }

        private static void Iterate(
            DiagMatrix matrix,
            IList<double> x,
            IReadOnlyList<double> f,
            IReadOnlyList<double> prevX,
            GaussMethodParams parameters,
            IReadOnlyList<int> indexes, 
            out double discrepancyNorm
            )
        {
            discrepancyNorm = 0;
            var n = x.Count;

            for (var i = 0; i < n; ++i)
            {
                double sum = 0;
                for (var j = 0; j < indexes.Count; ++j)
                {
                    var index = indexes[j] + i;
                    if (index < 0 || index >= n)
                        continue;

                    sum += prevX[index] * matrix.D[j][i];
                }

                discrepancyNorm += (f[i] - sum) * (f[i] - sum);
                x[i] = prevX[i] + (f[i] - sum) * parameters.Relaxation / matrix.D[4][i];
            }

            discrepancyNorm = Math.Sqrt(discrepancyNorm);
        }
    }
}
