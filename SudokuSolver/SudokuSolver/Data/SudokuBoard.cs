using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Data
{
    class SudokuBoard
    {
        private const int BOARD_ROW_SIZE = 9;
        private const int BOARD_COL_SIZE = 9;

        private int[,] _workBoard;
        private readonly int[,] _initialBoard;

        
        public SudokuBoard(int[,] initialBoard)
        {
            _initialBoard = initialBoard;
            _workBoard = _initialBoard;
        }

        public override string ToString()
        {
            StringBuilder boardOutput = new StringBuilder();
            for (int row = 0; row < _workBoard.GetLength(0); row++)
            {
                int[] rowElements = GetRow(row);
                boardOutput.AppendLine(RowToString(rowElements));
            }
            return boardOutput.ToString();
        }

        public int[] GetRow(int rowIndex)
        {
            int[] rowElements = new int[_workBoard.GetLength(1)];
            for (int colIndex = 0; colIndex < _workBoard.GetLength(1); colIndex++)
            {
                rowElements[colIndex] = _workBoard[rowIndex, colIndex];
            }
            return rowElements;
        }

        public static string RowToString(int[] rowElements)
        {
            StringBuilder sbuilder = new StringBuilder();
            sbuilder.Append("|");
            for (int index= 0; index < rowElements.Length; index++) 
            {
                sbuilder.Append($"{rowElements[index]}|");
            }
            return sbuilder.ToString();
       }

        public int[] GetCol(int colIndex)
        {
            int[] colElements = new int[_workBoard.GetLength(0)];
            for (int rowIndex = 0; rowIndex < _workBoard.GetLength(0); rowIndex++)
            {
                colElements[rowIndex] = _workBoard[rowIndex, colIndex];
            }

            return colElements;
        }
        public static string ColToString(int[] colElements)
        {
            StringBuilder sbuilder = new StringBuilder();
            for (int index = 0; index < colElements.Length; index++)
            {
                sbuilder.AppendLine($"|{colElements[index]}|");
            }
            return sbuilder.ToString();
        }
    }
}
