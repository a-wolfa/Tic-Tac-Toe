using Model;
using System;
using UnityEngine;

namespace Cell.Model
{
    [Serializable]
    public class CellModel
    {
        public int Row;
        public int Column;
        public PMove Move;

        public event Action<PMove> OnCellChanged;

        
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