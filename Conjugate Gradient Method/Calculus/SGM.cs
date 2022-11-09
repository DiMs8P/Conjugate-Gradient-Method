using Conjugate_Gradient_Method.Matrix;

namespace Conjugate_Gradient_Method.Calculus
{
    public static class SGM
    {
        public static double[] CalcX(SparseMatrix matrix, double[] initialX, double[] f,
            SparseMatrix factorizationMatrix, MethodParams methodParams)
        {
            double[] discrepancy = MultiplyFourMatrix(matrix, factorizationMatrix, CalcDiscrepancy(f, matrix.Multiply(initialX)));
            InitX(initialX, matrix.U);

            IterationVariables methodData = new (initialX, 0, discrepancy, discrepancy, 0);

            var fNorm = Math.Norm(f);
            var relativeDiscrepancy = Math.Norm(methodData.Discrepancy) / fNorm;

            var prevDiscDotProduct = Math.ScalarProduct(methodData.Discrepancy, methodData.Discrepancy);
            for (var k = 1; relativeDiscrepancy > methodParams.MinDiscrepancy && k < methodParams.MaxIterations; k++)
            {
                Iterate(matrix, factorizationMatrix, methodData, ref prevDiscDotProduct);
                relativeDiscrepancy = Math.Norm(methodData.Discrepancy) / fNorm;
                Console.WriteLine($"{k}: {relativeDiscrepancy}");
            }

            Sole.UpperTriangleInverseMethod(factorizationMatrix.U, factorizationMatrix.Diag, initialX,
                methodData.Solution);
            return initialX;
        }

        public static double[] CalcDiscrepancy(double[] vector, double[] vectorAbsolute)
        {
            return vector.Select((elem, index) => elem - vectorAbsolute[index]).ToArray();
        }

        private static void InitX(double[] initialX, SparseMatrixTriangle U)
        {
            double[] result = new double[initialX.Length];
            U.Multiply(initialX, result);
        }

        private static double[] MultiplyFourMatrix(SparseMatrix A, SparseMatrix factMatrix, double[] vector)
        {
            double[] result = new double[vector.Length];

            Sole.LowerTriangleInverseMethod(factMatrix.L, factMatrix.Diag, result, vector);
            Sole.TransposeLowerTriangleInverseMethod(factMatrix.L, factMatrix.Diag, vector, result);
            result = A.MultiplyTranspose(vector);
            Sole.TransposeUpperTriangleInverseMethod(factMatrix.U, factMatrix.Diag, vector, result);

            return vector;
        }

        private static double[] MultiplySixMatrix(SparseMatrix A, SparseMatrix factorizationMatrix, double[] vector)
        {
            double[] result = new double[vector.Length];
            Sole.UpperTriangleInverseMethod(factorizationMatrix.U, factorizationMatrix.Diag, result, vector);

            return MultiplyFourMatrix(A, factorizationMatrix, A.Multiply(result));
        }

        private static void Iterate(SparseMatrix A, SparseMatrix factMatrix, IterationVariables methodData, ref double prevDiscDotProduct)
        {
            methodData.Step = prevDiscDotProduct /
                              Math.ScalarProduct(
                                  MultiplySixMatrix(A, factMatrix, methodData.Descent),
                                  methodData.Descent);

            methodData.Solution = methodData.Descent
                .Select((elem, index) => elem * methodData.Step + methodData.Solution[index])
                .ToArray();

            methodData.Discrepancy = MultiplySixMatrix(A, factMatrix, methodData.Descent)
                .Select((elem, index) =>
                    methodData.Discrepancy[index] - elem * methodData.Step)
                .ToArray();

            var curDiscDotProduct = Math.ScalarProduct(methodData.Discrepancy, methodData.Discrepancy);
            methodData.Betta = curDiscDotProduct / prevDiscDotProduct;
            prevDiscDotProduct = curDiscDotProduct;

            methodData.Descent = methodData.Descent
                .Select((elem, index) => elem * methodData.Betta + methodData.Discrepancy[index])
                .ToArray();
        }
    }

}

