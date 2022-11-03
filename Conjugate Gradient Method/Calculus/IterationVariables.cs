namespace Conjugate_Gradient_Method.Calculus
{
    public class IterationVariables
    {
        public double[] Solution { get; set; }
        public double Step { get; set; }
        public double[] Discrepancy { get;  set;}
        public double[] Descent { get; set; }
        public double Betta { get; set; }

        public IterationVariables(
            double[] solution,
            double step,
            double[] discrepancy,
            double[] descent,
            double betta
        )
        {
            Solution = solution;
            Step = step;
            Discrepancy = discrepancy;
            Descent = descent;
            Betta = betta;
        }
    }
}