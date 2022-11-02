namespace Conjugate_Gradient_Method.Calculus
{
    public static class Solution
    {
        public static double[] CalcX(SparseMatrix matrix, double[] initialX, double[] f,
            SparseMatrix factorizationMatrix, MethodParams methodParams)
        {
            double[] discrepancy = MultiplyFourFuckingMatrix(matrix, factorizationMatrix, CalcDiscrepancy(f, matrix.Multiply(initialX)));
            InitX(initialX, matrix.U);

            IterationVariables methodData = new IterationVariables(initialX, 0, discrepancy, discrepancy, 0);

            var fNorm = Math.Norm(f);
            var relativeDiscrepancy = Math.Norm(discrepancy) / fNorm;

            for (var k = 1; relativeDiscrepancy > methodParams.MinDiscrepancy || k < methodParams.MaxIterations; k++)
            {
                Iterate(matrix, factorizationMatrix, methodData);
                relativeDiscrepancy = Math.Norm(discrepancy) / fNorm;
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

        private static double[] MultiplyFourFuckingMatrix(SparseMatrix A, SparseMatrix factMatrix, double[] vector)
        {
            double[] result = new double[vector.Length];

            Sole.LowerTriangleInverseMethod(factMatrix.L, factMatrix.Diag, result, vector);
            Sole.TransposeLowerTriangleInverseMethod(factMatrix.L, factMatrix.Diag, vector, result);
            result = A.MultiplyTranspose(vector);
            Sole.TransposeUpperTriangleInverseMethod(factMatrix.U, factMatrix.Diag, vector, result);

            return vector;
        }

        private static double[] MultiplySixFuckingMatrix(SparseMatrix A, SparseMatrix factorizationMatrix, double[] vector)
        {
            double[] result = new double[vector.Length];
            Sole.UpperTriangleInverseMethod(factorizationMatrix.U, factorizationMatrix.Diag, result, vector);

            return MultiplyFourFuckingMatrix(A, factorizationMatrix, A.Multiply(result));
        }

        private static void Iterate(SparseMatrix A, SparseMatrix factorizationMatrix, IterationVariables methodData)
        {
            var discrepancyScalarProduct = Math.ScalarProduct(methodData.Discrepancy, methodData.Discrepancy);
            methodData.Step = discrepancyScalarProduct /
                                Math.ScalarProduct(
                                    MultiplySixFuckingMatrix(A, factorizationMatrix, methodData.Descent),
                                    methodData.Descent);

            methodData.Solution = methodData.Descent.Select((elem, index) => elem * methodData.Step + methodData.Solution[index]).ToArray();

            methodData.Discrepancy = MultiplySixFuckingMatrix(A, factorizationMatrix, methodData.Descent)
                .Select((elem, index) =>
                    methodData.Descent[index] - elem * methodData.Step)
                .ToArray();

            methodData.Betta = Math.ScalarProduct(methodData.Discrepancy, methodData.Discrepancy) / discrepancyScalarProduct;

            methodData.Descent = methodData.Descent
                .Select((elem, index) => elem * methodData.Betta + methodData.Discrepancy[index])
                .ToArray();
        }
    }


}

