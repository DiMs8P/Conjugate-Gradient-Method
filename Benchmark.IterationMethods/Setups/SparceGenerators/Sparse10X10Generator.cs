using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conjugate_Gradient_Method.IO;
using Conjugate_Gradient_Method.Matrix;

namespace Benchmark.IterationMethods.Setups.SparceGenerators
{
    internal class Sparse10X10Generator
    {
        private SparseMatrix _matrix;
        public SparseMatrix Matrix { get { return _matrix; } }
        public Sparse10X10Generator()
        {
            SparseMatrixReader reader = new SparseMatrixReader(
                new SparseMatrixFilesProvider(
                    "di.txt",
                    "ggl.txt",
                    "igl.txt",
                    "jgl.txt",
                    "ggu.txt",
                    "igu.txt",
                    "jgu.txt"
                ),
                "C:\\Users\\Dima\\Desktop\\Input\\SparseInput\\");

            _matrix = reader.Read();
        }
    }
}
