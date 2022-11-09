namespace GaussMethod.Logging
{
    public class EveryKthIterationLogger : IIterationMethodLogger
    {
        private readonly int _k;

        public EveryKthIterationLogger(int k)
        {
            if (k <= 0) throw new ArgumentOutOfRangeException(nameof(k));

            _k = k;
        }

        public void Log(int iterationNumber, double relativeDiscrepancy)
        {
            if (iterationNumber % _k == 0)
                Console.WriteLine(FormatIteration(iterationNumber, relativeDiscrepancy));
        }

        public void LogFinish(int iterationNumber, double relativeDiscrepancy)
        {
            Console.WriteLine(FormatIteration(iterationNumber, relativeDiscrepancy));
            Console.WriteLine("Finish!");
        }

        private static string FormatIteration(int iterationNumber, double relativeDiscrepancy)
        {
            return $"[{iterationNumber}]: {relativeDiscrepancy}";
        }
    }
}
