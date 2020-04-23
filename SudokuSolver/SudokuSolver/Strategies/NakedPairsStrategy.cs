﻿using SudokuSolver.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Strategies
{
    class NakedPairsStrategy : ISudokuStrategy
    {
        private readonly SudokuMapper _sudokuMapper;

        public NakedPairsStrategy(SudokuMapper sudokuMapper)
        {
            _sudokuMapper = sudokuMapper;
        }

        public int[,] Solve(int[,] sudokuBoard)
        {
            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                for (int col = 0; col < sudokuBoard.GetLength(1); col++)
                {
                    EliminateNakedPairFromOthersInRow(sudokuBoard, row, col);
                    EliminateNakedPairFromOthersInCol(sudokuBoard, row, col);
                    EliminateNakedPairFromOthersInBlock(sudokuBoard, row, col);
                }
            }
            return sudokuBoard;
        }

        private void EliminateNakedPairFromOthersInRow(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            if (!HasNakedPairInRow(sudokuBoard, givenRow, givenCol)) return;
            for (int col = 0; col < sudokuBoard.GetLength(1); col++)
            {
                //if there are possibilities eliminate naked pair
                if (sudokuBoard[givenRow, col] != sudokuBoard[givenRow, givenCol] && sudokuBoard[givenRow, col].ToString().Length > 1)
                {
                    EliminateNakedPair(sudokuBoard, sudokuBoard[givenRow, givenCol], givenRow, col);
                }
            }
        }

        private void EliminateNakedPairFromOthersInCol(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            if (!HasNakedPairInCol(sudokuBoard, givenRow, givenCol)) return;
            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                //if there are possibilities eliminate naked pair
                if (sudokuBoard[row, givenCol] != sudokuBoard[givenRow, givenCol] && sudokuBoard[row, givenCol].ToString().Length > 1)
                {
                    EliminateNakedPair(sudokuBoard, sudokuBoard[givenRow, givenCol], row, givenCol);
                }
            }
        }


        private void EliminateNakedPairFromOthersInBlock(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            if (!HasNakedPairInBlock(sudokuBoard, givenRow, givenCol)) return;
            var sudokuMap = _sudokuMapper.Find(givenRow, givenCol);
            for (int row = sudokuMap.StartRow; row < sudokuMap.StartRow + 3; row++)
            {
                for (int col = sudokuMap.StartCol; col < sudokuMap.StartCol + 3; col++)
                {
                    if (sudokuBoard[row,col].ToString().Length > 1 && sudokuBoard[row,col] != sudokuBoard[givenRow, givenCol])
                    {
                        EliminateNakedPair(sudokuBoard, sudokuBoard[givenRow, givenCol], row, col);
                    }
                }
            }
        }


        private bool HasNakedPairInRow(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            for (int col = 0; col < sudokuBoard.GetLength(1); col++)
            {
                if (givenCol != col && IsNakedPair(sudokuBoard[givenRow, col], sudokuBoard[givenRow, givenCol]))
                {
                    return true;
                }
            }
            return false;
        }

        private bool HasNakedPairInCol(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                if (givenRow != row && IsNakedPair(sudokuBoard[row, givenCol], sudokuBoard[givenRow, givenCol]))
                {
                    return true;
                }
            }
            return false;
        }

        private bool HasNakedPairInBlock(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            var sudokuMap = _sudokuMapper.Find(givenRow, givenCol);
            for (int row = sudokuMap.StartRow; row < sudokuMap.StartRow + 3; row++)
            {
                for (int col = sudokuMap.StartCol; col < sudokuMap.StartCol + 3; col++)
                {
                    bool isSameCell = (givenRow == row) && (givenCol == col);
                    if (!isSameCell && IsNakedPair(sudokuBoard[row, givenCol], sudokuBoard[givenRow, givenCol]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsNakedPair(int firstPair, int secondPair)
        {
            return firstPair.ToString().Length == 2 && firstPair == secondPair;
        }

        private void EliminateNakedPair(int[,] sudokuBoard, int valuesToEliminate, int eliminateFromRow, int eliminateFromCol)
        {
           var valuesToEliminateArray = valuesToEliminate.ToString().ToCharArray();
            foreach (var valueToEliminate in valuesToEliminateArray)
            {
                sudokuBoard[eliminateFromRow, eliminateFromCol] = Convert.ToInt32(sudokuBoard[eliminateFromRow, eliminateFromCol].ToString().Replace(valueToEliminate.ToString(),string.Empty));
            }
        }
    }
}
