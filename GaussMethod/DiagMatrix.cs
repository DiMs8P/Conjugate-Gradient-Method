using GaussMethod.IO;

namespace GaussMethod
{
    public class DiagMatrix
    {
        public const int DiagsCount = 9;
        public const int DiagsGroupSize = 3;
        public double[][] D { get; }

        private readonly int _diagsShift;

        public DiagMatrix(MatrixReader reader, int diagsShift)
        {
            _diagsShift = diagsShift;
            D = reader.Read();
        }

        public DiagMatrix(double[][] diags, int diagsShift=0)
        {
            _diagsShift = diagsShift;
            D = diags;
        }

        public void GetIndexes(int[] indexes)
        {
            int firstIndexValue = -(_diagsShift + 1 + 3);

            for (int i = 0; i < DiagsCount; i += DiagsGroupSize)
                for (int j = 0; j < DiagsGroupSize; ++j)
                    indexes[i + j] =
                        firstIndexValue +
                        _diagsShift * i / DiagsGroupSize +
                        i + j;
        }
    }
}
