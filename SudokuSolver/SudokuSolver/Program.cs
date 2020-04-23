using SudokuSolver.Data;
using SudokuSolver.Strategies;
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
            try
            {
                SudokuMapper sudokuMapper = new SudokuMapper();
                SudokuBoardStateManager sudokuBoardStateManager = new SudokuBoardStateManager();
                SudokuSolverEngine sudokuSolverEngine = new SudokuSolverEngine(sudokuBoardStateManager, sudokuMapper);
                //SudokuFileReader sudokuFileReader = new SudokuFileReader();
                SudokuBoardDisplayer sudokuBoardDisplayer = new SudokuBoardDisplayer();

                Console.WriteLine("Please enter the filename containing the sudoku Puzzle");
                var filename = "SudokuEasy.txt"; //Console.ReadLine();

                int[,] sudokuBoard = SudokuFileReader.ReadFile(filename);
                sudokuBoardDisplayer.Display("Initial State", sudokuBoard);

                bool isSudokuSolved = sudokuSolverEngine.Solve(sudokuBoard);
                sudokuBoardDisplayer.Display("Final State", sudokuBoard);

                Console.WriteLine(isSudokuSolved
                    ? "You have succesfully solved this Sudoku Puzzle!"
                    : "Unfortunately, current algorithm(s) were not enough to solve the current Sudoku Puzzle!");
            }
            catch (Exception ex)
            {
                // In real world we would want to log this message
                Console.WriteLine("{0} : {1}", "Sudoku Puzzle cannot be solved because there was an error: ", ex.Message);
            }
            //var sudokuBoard = SudokuFileReader.ReadFile(@"C:\Temp\SudokuSolverTestFile\SudokuEasy.txt");
            //SudokuBoard sudoku = new SudokuBoard(sudokuBoard);
            //Console.WriteLine(sudoku.ToString());
            //Console.ReadKey();
        }
    }
}
