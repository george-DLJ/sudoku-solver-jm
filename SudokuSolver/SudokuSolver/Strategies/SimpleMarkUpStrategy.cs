using SudokuSolver.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Strategies
{
    class SimpleMarkUpStrategy : ISudokuStrategy
    {
        private readonly SudokuMapper _sudokuMapper;

        public SimpleMarkUpStrategy(SudokuMapper sudokuMapper)
        {
            _sudokuMapper = sudokuMapper;
        }

        /// <summary>
        /// For each unsolved cell, find intersection of the possibilities  of:
        ///  - Row
        ///  - Column
        ///  - Block
        /// </summary>
        /// <param name="sudokuBoard"></param>
        /// <returns></returns>
        public int[,] Solve(int[,] sudokuBoard)
        {

            try
            {
                for (int row = 0; row < sudokuBoard.GetLength(0); row++)
                {
                    for (int col = 0; col < sudokuBoard.GetLength(1); col++)
                    {
                        if (sudokuBoard[row, col] == 0 || sudokuBoard[row, col].ToString().Length > 1)
                        {
                            int possibilitiesInRowAndCol = GetPossibilitiesInRowAndCol(sudokuBoard, row, col);
                            int possibilitiesInBlock = GetPossibilitiesInBlock(sudokuBoard, row, col);
                            sudokuBoard[row, col] = GetPossibilityIntersection(possibilitiesInRowAndCol, possibilitiesInBlock);
                        }
                    }

                }

                return sudokuBoard;
            }
            catch (Exception ex)
            {
                throw new Exception("Error xxxx():" + ex.Message);
            }
        }

        /// <summary>
        /// See: for an alternative implementaion. 
        /// //// see: https://stackoverflow.com/a/4343348
        /// </summary>
        /// <param name="sudokuBoard"></param>
        /// <param name="givenRow"></param>
        /// <param name="givenCol"></param>
        /// <returns></returns>
        private int GetPossibilitiesInRowAndCol(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            try
            {
                // Avetis solution:
                int[] possibilities = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                for (int col = 0; col < 9; col++)
                {
                    if (IsValidSingle(sudokuBoard[givenRow, col]))
                    {
                        possibilities[sudokuBoard[givenRow, col] - 1] = 0;
                    }
                }
                for (int row = 0; row < 9; row++)
                {
                    if (IsValidSingle(sudokuBoard[givenCol, row]))
                    {
                        possibilities[sudokuBoard[givenCol, row] - 1] = 0;
                    }
                }
                return Convert.ToInt32(String.Join(string.Empty, possibilities.Select(p => p).Where(p => p != 0)));
            }
            catch (Exception ex)
            {
                throw new Exception("Error xxxx():" + ex.Message);
            }

            //// Alternative: 
            //// Get row possibilities and delete all possibilities that are not in common with col-possiblities.
            //int[] rowPossibilities  = sudokuBoard.i
            //    int[] colPossibilities = sudokuBoard.
            //// Get col possibilities

            //// return common possibilites
            //// see: https://stackoverflow.com/a/4343348
        }



        private int GetPossibilitiesInBlock(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            try
            {
                int[] possibilities = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                var sudokuMap = _sudokuMapper.Find(givenRow, givenCol);

                for (int row = sudokuMap.StartRow; row < sudokuMap.StartRow + 3; row++)
                {
                    for (int col = sudokuMap.StartCol; col < sudokuMap.StartCol + 3; col++)
                    {
                        if (IsValidSingle(sudokuBoard[row, col]))
                        {
                            possibilities[sudokuBoard[row, col] - 1] = 0;
                        }

                    }
                }
                return Convert.ToInt32(String.Join(string.Empty, possibilities.Select(p => p).Where(p => p != 0)));

            }
            catch(Exception ex)
            {
                throw new Exception("Error GetPossibilitiesInBlock():" + ex.Message);
            }

        }

        private int GetPossibilityIntersection(int possibilitiesInRowAndCol, int possibilitiesInBlock)
        {

            try
            {
                var possibilitiesInRowAndColCharArray = possibilitiesInRowAndCol.ToString().ToCharArray();
                var possibilitiesInBlockCharArray = possibilitiesInBlock.ToString().ToCharArray();
                var possibilitiesSubset = possibilitiesInRowAndColCharArray.Intersect(possibilitiesInBlockCharArray);
                return Convert.ToInt32(String.Join(string.Empty, possibilitiesSubset));
            }
            catch (Exception ex)
            {
                throw new Exception("Error xxxx():" + ex.Message);
            }
        }

        private bool IsValidSingle(int cellDigit)
        {
            try
            {
                return cellDigit != 0 && cellDigit.ToString().Length == 1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error xxxx():" + ex.Message);
            }
        }
    }
}
