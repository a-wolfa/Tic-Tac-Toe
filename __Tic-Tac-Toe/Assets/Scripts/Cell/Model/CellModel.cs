using Model;
using System;

namespace Cell.Model
{
    [Serializable]
    public class CellModel
    {
        public int Row;
        public int Column;

        public event Action<PlayerMove> OnCellChanged;

        public PlayerMove PlayerMove { get; set; }
        
        public CellModel(int row, int column)
        {
            this.Row = row;
            this.Column = column;
            this.PlayerMove = PlayerMove.GameOver;
        }

        public void HandleCellChanged(PlayerMove cellType)
        {
            PlayerMove = cellType;
            OnCellChanged?.Invoke(PlayerMove);
        }

    }
}