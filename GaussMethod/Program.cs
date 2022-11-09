using Conjugate_Gradient_Method.IO;
using GaussMethod.Generators;
using GaussMethod.IO;
using GaussMethod.Logging;
using GaussMethod.Types;

namespace GaussMethod
{
    public class Program
    {
        private const string Root = "../../../Input/";
        public static void Main(string[] args)
        {
            DoubleVectorReader fReader = new("F.txt", Root);
            DoubleVectorReader xReader = new("x.txt", Root);
            MatrixReader matrixReader = new("A.txt", Root);

            const int diagsShift = 2;
            MethodData parameters = new(
                Accuracy: 0.000000000001,
                Relaxation: 1.025,
                MaxIteration: 30000
            );

            var f = fReader.Read().ToArray();
            var x = xReader.Read().ToArray();
            DiagMatrix A = new(matrixReader, diagsShift);

            IterationSolver solver = new(new EveryKthIterationLogger(1000));

            var solution = solver.GaussMethod(
                matrix: A,
                x: x,
                f: f,
                parameters: parameters
            );

            Console.WriteLine();
            Console.WriteLine("Solution:");

            foreach (var elem in solution)
                Console.WriteLine(elem);

            var t = Gilbert5X5TestGenerator.GetTest();
        }

    }
}