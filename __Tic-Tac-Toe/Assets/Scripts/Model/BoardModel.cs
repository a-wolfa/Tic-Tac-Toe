using System.Linq;
using UnityEngine;

namespace Model
{
    public class BoardModel
    {
        private Cell[,] _cBoard = new Cell[3, 3];

        public void SetCell(int row, int column, Cell selectedCell)
        {
            _cBoard[row, column] = selectedCell;
            Debug.Log(selectedCell.playedPlayer);
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

        public void Reset()
        {
            _cBoard = new Cell[3,3];
        }
    }
}
