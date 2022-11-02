namespace Conjugate_Gradient_Method.Calculus
{
    public readonly record struct IndexValue(int Index, double Value);

    public readonly record struct MethodParams(int MaxIterations, double MinDiscrepancy);
}
