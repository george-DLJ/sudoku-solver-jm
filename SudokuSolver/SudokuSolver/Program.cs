using SudokuSolver.Data;
using SudokuSolver.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var sudokuBoard = SudokuFileReader.ReadFile(@"C:\Temp\SudokuSolverTestFile\SudokuEasy.txt");
            SudokuBoard sudoku = new SudokuBoard(sudokuBoard);
            Console.WriteLine(sudoku.ToString());
            Console.ReadKey();
        }
    }
}
