namespace GaussMethod.Types
{
    public record GaussMethodParams(
        double Accuracy,
        double Relaxation,
        int MaxIteration
    );
}