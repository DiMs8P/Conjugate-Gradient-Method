namespace Conjugate_Gradient_Method.Calculus
{
    public record struct IterationVariables(
        double[] Solution,
        double Step,
        double[] Discrepancy,
        double[] Descent,
        double Betta
            );
}