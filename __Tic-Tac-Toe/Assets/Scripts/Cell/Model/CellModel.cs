using Model;
using System;

namespace Cell.Model
{
    [Serializable]
    public class CellModel
    {
        public int Row;
        public int Column;

        public event Action<PMove> OnCellChanged;

        public PMove Move { get; set; }
        
        public CellModel(int row, int column)
        {
            this.Row = row;
            this.Column = column;
            this.Move = PMove.None;
        }

        public void HandleCellChanged(PMove cellType)
        {
            Move = cellType;
            OnCellChanged?.Invoke(Move);
        }

    }
}