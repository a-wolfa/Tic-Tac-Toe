using Cell.Model;
using Model;
using System;
using UnityEngine;


namespace Board.Model
{
    public class BoardModel
    {
        private int _size = 3;
        
        private CellModel[,] _cellModels;

        public BoardModel(CellModel[,] cells)
        {
            _cellModels = cells;
        }

        public CellModel GetCell(int row, int column)
        {
            return IsValidPosition(row, column) ? _cellModels[row, column] : null;
        }

        public CellModel[,] GetBoard()
        {
            return _cellModels;
        }

        public void ResetBoard()
        {
            for (int row = 0; row < _size; row++)
            {
                for (int column = 0; column < _size; column++)
                {
                    _cellModels[row, column].HandleCellChanged(PMove.None);
                }
            }
        }

        private bool IsValidPosition(int row, int column)
        {
            return row >= 0 && row < Size && column >= 0 && column < Size;
        }

        public PMove CheckWin()
        {

            for (int row = 0; row < _size; row++)
            {
                if (IsWinningLine(_cellModels[row, 0], _cellModels[row, 1], _cellModels[row, 2]))
                    return _cellModels[row, 0].Move;
            }

            for (int column = 0; column < _size; column++)
            {
                if (IsWinningLine(_cellModels[0, column], _cellModels[1, column], _cellModels[2, column]))
                    return _cellModels[0, column].Move;
            }

            if (IsWinningLine(_cellModels[0, 0], _cellModels[1, 1], _cellModels[2, 2]))
                return _cellModels[0, 0].Move;

            if (IsWinningLine(_cellModels[0, 2], _cellModels[1, 1], _cellModels[2, 0]))
                return _cellModels[0, 2].Move;

            return PMove.None;
        }

        private bool IsWinningLine(CellModel a, CellModel b, CellModel c)
        {
            if (a.Move == PMove.None)
                return false;

            return a.Move == b.Move && b.Move == c.Move;
        }

        public int Size => _size;
    }
}
