using System.Linq;
using UnityEngine;

namespace Model
{
    public class BoardModel
    {
        private Cell[,] _cBoard;

        public BoardModel(Cell[,] slots)
        {
            _cBoard = slots;
        }

        public void SetCell(int row, int column, Cell selectedCell)
        {
            _cBoard[row, column] = selectedCell;
        }

        public Cell GetCell(int row, int column)
        {
            return _cBoard[row, column];
        }
        
        public void SetBoard(Cell[,] board)
        {
            _cBoard = board;
        }

        public Cell[,] GetBoard()
        {
            return _cBoard;
        }
    }
}
