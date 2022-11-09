namespace GaussMethod.Logging
{
    public class DisabledLogger : IIterationMethodLogger
    {
        public void Log(int iterationNumber, double relativeDiscrepancy) { }

        public void LogFinish(int iterationNumber, double relativeDiscrepancy) { }
    }
}
