using System;

namespace Conjugate_Gradient_Method.Calculus

{
    public static class Math
    {

        public static double ScalarProduct(double[] x, double[] y)
        {
            if (x.Length != y.Length) throw new IndexOutOfRangeException(nameof(x));

            double result = 0;

            for (int i = 0; i < x.Length; i++)
            {
                result += x[i] * y[i];
            }

            return result;
        }

        public static double Norm(double[] vector)
        {
            double result = 0;
            foreach (var elem in vector)
            {
                result += elem * elem;
            }

            return System.Math.Sqrt(result);
        }
    }
}
