using Conjugate_Gradient_Method.Matrix;

namespace Benchmark.IterationMethods.Setups.SparceGenerators
{
    public class SparseHilbertGenerator
    {
        public static SparseMatrix Generate(int Size)
        {
            return new SparseMatrix(LoverTriangle(Size), UpperTriangle(Size), Diag(Size));
        }

        private static SparseMatrixTriangle LoverTriangle(int size)
        {
            double[] values = new double[(size * size - size) / 2];
            for (int i = 0, index = 0; i < size; i++)
            {
                for (int j = 0; j < i; j++, index++)
                {
                    values[index] = 1d / (1 + i + j);
                }
            }

            int[] lowerRowPrt = new int[size];
            for (int i = 1; i < size; i++)
            {
                lowerRowPrt[i] = i + lowerRowPrt[i - 1];
            }

            int[] lowerRowColumn = new int[(size * size - size) / 2];
            for (int i = 1, index = 0; i < size; i++)
            {
                for (int j = 0; j < i; j++, index++)
                {
                    lowerRowColumn[index] = j;
                }
            }

            return new SparseMatrixTriangle(values, lowerRowPrt, lowerRowColumn);
        }

        private static SparseMatrixTriangle UpperTriangle(int Size)
        {
            double[] values = new double[(Size * Size - Size) / 2];
            for (int i = 0, index = 0; i < Size - 1; i++)
            {
                for (int j = i + 1; j < Size; j++, index++)
                {
                    values[index] = 1d / (1 + i + j);
                }
            }

            int[] upperRowPrt = new int[Size];
            upperRowPrt[0] = Size - 1;
            for (int i = 1; i < Size; i++)
            {
                upperRowPrt[i] =  Size - i - 1 + upperRowPrt[i - 1];
            }

            int[] upperRowColumn = new int[(Size * Size - Size) / 2];
            for (int i = 0, index = 0; i < Size - 1; i++)
            {
                for (int j = i + 1; j < Size; j++, index++)
                {
                    upperRowColumn[index] = j;
                }
            }

            return new SparseMatrixTriangle(values, upperRowPrt, upperRowColumn);
        }

        private static double[] Diag(int Size)
        {
            double[] diag = new double[Size];
            for (int i = 0; i < Size; i++)
            {
                diag[i] = 1d / (2 * i + 1);
            }
            return diag;
        }
    }
}
