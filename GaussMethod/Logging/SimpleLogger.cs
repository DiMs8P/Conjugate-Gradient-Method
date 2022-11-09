namespace GaussMethod.Logging
{
    internal class SimpleLogger : EveryKthIterationLogger
    {
        public SimpleLogger() 
            : base(1)
            { }
    }
}
