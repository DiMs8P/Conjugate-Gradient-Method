namespace Conjugate_Gradient_Method.IO
{
    public readonly record struct SparseMatrixFilesProvider(
        string DI,
        
        string GGL,
        string IGL,
        string JGL,

        string GGU,
        string IGU,
        string JGU
    );
}
