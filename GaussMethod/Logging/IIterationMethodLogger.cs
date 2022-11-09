namespace GaussMethod.Logging
{
    public interface IIterationMethodLogger
    {
        public void Log(int iterationNumber, double relativeDiscrepancy);
        public void LogFinish(int iterationNumber, double relativeDiscrepancy);
    }
}
