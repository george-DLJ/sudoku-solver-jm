using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Workers
{
    class SudokuFileReader
    {
        public static int[,] ReadFile(string filename)
        {
            int[,] sudokuBoard = new int[9, 9];
            try
            {
                var sudokuBoardLines = File.ReadAllLines(filename);
                int row = 0;
                foreach(var sudokuBoardLine in sudokuBoardLines)
                {
                    string[] sudokuBoardLineElements = sudokuBoardLine.Split('|').Skip(1).Take(9).ToArray();
                    int col = 0;
                    foreach(var element in sudokuBoardLineElements)
                    {
                        sudokuBoard[row, col] = element.Equals(" ") ? 0 : Convert.ToInt16(element);
                        col++;
                    }
                    row++;
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error reading file:" + ex.Message);
            }
            return sudokuBoard;
        }
    }
}
