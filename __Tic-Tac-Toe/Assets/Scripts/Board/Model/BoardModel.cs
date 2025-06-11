using Cell.Controllers;
using Cell.Model;
using UnityEngine;

namespace Board.Model
{
    public class BoardModel
    {
        private readonly int _rows;
        private readonly int _columns;
        private CellController[,] _cellControllers;

        public BoardModel(int rows = 3, int columns = 3)
        {
            _rows = rows;
            _columns = columns;
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            _cellControllers = new CellController[_rows, _columns];
        }

        public void InitializeCell(int row, int column, CellController cellController)
        {
            if (IsValidPosition(row, column))
            {
                _cellControllers[row, column] = cellController;
            }
        }

        public void SetCell(int row, int column, CellController selectedCell)
        {
            if (IsValidPosition(row, column))
            {
                _cellControllers[row, column] = selectedCell;
            }
        }

        public CellController GetCell(int row, int column)
        {
            return IsValidPosition(row, column) ? _cellControllers[row, column] : null;
        }
        
        public CellController[,] GetBoard()
        {
            return _cellControllers;
        }

        public void ResetBoard()
        {
            InitializeBoard();
        }

        private bool IsValidPosition(int row, int column)
        {
            return row >= 0 && row < _rows && column >= 0 && column < _columns;
        }

        public int Rows => _rows;
        public int Columns => _columns;
    }
}
