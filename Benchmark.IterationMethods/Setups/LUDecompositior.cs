using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Columns;
using Conjugate_Gradient_Method.Calculus;
using Conjugate_Gradient_Method.Matrix;

namespace Benchmark.IterationMethods.Setups
{
    public class LUDecompositor
    {
        private SparseMatrix _sorceMatrix;

        public LUDecompositor(SparseMatrix sorceMatrix)
        {
            _sorceMatrix = new SparseMatrix(sorceMatrix.L, sorceMatrix.U, sorceMatrix.Diag);
        }

        public SparseMatrix Decompose()
        {
            /*var Diag = new double[_sorceMatrix.Diag.Length];

            var L = new SparseMatrixTriangle(
                new double[_sorceMatrix.L.Values.Length],
                _sorceMatrix.L.RowPtr.ToArray(),
                _sorceMatrix.L.ColumnPtr.ToArray()
            );

            var U = new SparseMatrixTriangle(
                new double[_sorceMatrix.U.Values.Length],
                _sorceMatrix.U.RowPtr.ToArray(),
                _sorceMatrix.U.ColumnPtr.ToArray()
            );*/



            for (int i = 0; i < _sorceMatrix.Diag.Length; i++)
            {

                //I < J
                foreach (var (columnIndex, value) in _sorceMatrix.L.ColumnIndexValuesByRow(i))
                {
                    double sum = 0;
                    foreach (var (columnIndexI, valueI) in _sorceMatrix.L.ColumnIndexValuesByRow(i))
                    {
                        if (columnIndexI >= columnIndex)
                            break;

                        foreach (var (columnIndexJ, valueJ) in _sorceMatrix.U.ColumnIndexValuesByRow(columnIndexI))
                        {
                            if (columnIndexJ > columnIndex)
                                break;

                            if (columnIndexJ == columnIndex)
                            {
                                sum += valueJ * valueI;
                                break;
                            }
                        }
                    }
                    _sorceMatrix.L[i, columnIndex] = (_sorceMatrix.L[i, columnIndex] - sum) / _sorceMatrix.Diag[columnIndex];
                }

                //DIAG
                double sumDiag = 0;
                foreach (var (columnIndexI, valueI) in _sorceMatrix.L.ColumnIndexValuesByRow(i))
                {
                    foreach (var (columnIndexJ, valueJ) in _sorceMatrix.U.ColumnIndexValuesByRow(columnIndexI))
                    {
                        if (columnIndexJ > i)
                            break;

                        if (columnIndexJ == i)
                        {
                            sumDiag += valueJ * valueI;
                            break;
                        }
                    }
                }
                _sorceMatrix.Diag[i] = _sorceMatrix.Diag[i] - sumDiag;


                //I>J
                foreach (var (columnIndex, value) in _sorceMatrix.U.ColumnIndexValuesByRow(i))
                {
                    double sum = 0;
                    foreach (var (columnIndexI, valueI) in _sorceMatrix.L.ColumnIndexValuesByRow(i))
                    {
                        if (columnIndexI >= columnIndex)
                            break;

                        foreach (var (columnIndexJ, valueJ) in _sorceMatrix.U.ColumnIndexValuesByRow(columnIndexI))
                        {
                            if (columnIndexJ > columnIndex)
                                break;

                            if (columnIndexJ == columnIndex)
                            {
                                sum += valueJ * valueI;
                                break;
                            }
                        }
                    }
                    _sorceMatrix.U[i, columnIndex] = _sorceMatrix.U[i, columnIndex] - sum;
                }
            }
            return _sorceMatrix;
        }
    }
}
