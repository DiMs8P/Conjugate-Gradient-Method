using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Benchmark.IterationMethods.Setups;
using Benchmark.IterationMethods.Setups.SparceGenerators;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Reports;
using Conjugate_Gradient_Method.Calculus;
using Conjugate_Gradient_Method.IO;
using Conjugate_Gradient_Method.Matrix;

namespace Benchmark.IterationMethods
{
    public class SMGLargeMatrixBenchmark
    {
        private SparseMatrix _matrix;
        private double[] _initialX;
        private SparseMatrix _factMatrix1;
        private SparseMatrix _factMatrix2;
        private SparseMatrix _factMatrix3;
        private double[] f;
        private MethodParams _params;

        //[Params("945\\", "4545\\")]
        public string folder = "945\\";
        public SMGLargeMatrixBenchmark()
        {

            InitMatrix();

            InitFactMatrix();

            _initialX = new double[_matrix.Diag.Length];

            InitF();

            _params = new MethodParams(30000, 0.000000000001);
        }

        private void InitMatrix()
        {
            SparseMatrixReader reader = new SparseMatrixReader(
                new SparseMatrixFilesProvider(
                    "diag.txt",
                    "ggl.txt",
                    "igl.txt",
                    "jgl.txt",
                    "ggu.txt",
                    "igu.txt",
                    "jgu.txt"
                ),
                Program.RootPath + "Large\\" + folder);

            _matrix = reader.Read();
        }

        private void InitFactMatrix()
        {
            _factMatrix2 = new SparseMatrix(_matrix.Diag);

            double[] identity = _matrix.Diag.Select(x => 1.0).ToArray();
            _factMatrix1 = new SparseMatrix(identity);

            LUDecompositor decompositor = new LUDecompositor(_matrix);
            _factMatrix3 = decompositor.Decompose();
        }
        private void InitF()
        {
            DoubleVectorReader reader = new DoubleVectorReader("f.txt", Program.RootPath + "Large\\" + folder);
            f = reader.Read().ToArray();
        }


        [Benchmark]
        public double[] IdentityMatrix()
        {
            return SGM.CalcX(_matrix, _initialX, f, _factMatrix1, _params);
        }

        [Benchmark]
        public double[] DiagMatrix()
        {
            return SGM.CalcX(_matrix, _initialX, f, _factMatrix2, _params);
        }

        [Benchmark]
        public double[] LUDecomposition()
        {
            return SGM.CalcX(_matrix, _initialX, f, _factMatrix3, _params);
        }
    }
}
