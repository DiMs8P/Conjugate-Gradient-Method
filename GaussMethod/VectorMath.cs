namespace GaussMethod
{
    public static class VectorMath
    {
        public static double GetNorm(double[] x)
        {
            var norm = x.Sum(elem => elem * elem);

            norm = Math.Sqrt(norm);

            return norm;
        }
}
}
