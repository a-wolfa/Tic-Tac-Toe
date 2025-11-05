using System;

namespace Core
{
    [Serializable]
    public class Board
    {
        private Cell[,] _cells;

        public Board()
        {
            _cells= new Cell[3, 3];
        }

        public PlayerType CheckWinner()
        {
            // Check rows and columns
            for (int i = 0; i < 3; i++)
            {
                // Rows
                var firstCellInRow = this[0, i];
                if (firstCellInRow.PlayerMarked != PlayerType.None &&
                    this[1, i].PlayerMarked == firstCellInRow.PlayerMarked &&
                    this[2, i].PlayerMarked == firstCellInRow.PlayerMarked)
                    return firstCellInRow.PlayerMarked;

                // Columns
                var firstCellInColumn = this[i, 0];
                if (firstCellInColumn.PlayerMarked != PlayerType.None &&
                    this[i, 1].PlayerMarked == firstCellInColumn.PlayerMarked &&
                    this[i, 2].PlayerMarked == firstCellInColumn.PlayerMarked)
                    return firstCellInColumn.PlayerMarked;
            }

            // Main diagonal
            var firstDiagCell = this[0, 0];
            if (firstDiagCell.PlayerMarked != PlayerType.None &&
                this[1, 1].PlayerMarked == firstDiagCell.PlayerMarked &&
                this[2, 2].PlayerMarked == firstDiagCell.PlayerMarked)
                return firstDiagCell.PlayerMarked;

            // Anti-diagonal
            var firstAntiDiagCell = this[0, 2];
            if (firstAntiDiagCell.PlayerMarked != PlayerType.None &&
                this[1, 1].PlayerMarked == firstAntiDiagCell.PlayerMarked &&
                this[2, 0].PlayerMarked == firstAntiDiagCell.PlayerMarked)
                return firstAntiDiagCell.PlayerMarked;

            // No winner
            return PlayerType.None;
        }
        
        public Cell this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }
    }
}