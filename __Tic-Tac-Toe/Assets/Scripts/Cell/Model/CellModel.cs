using System;

namespace Cell.Model
{
    [Serializable]
    public class CellModel
    {
        public int Row;
        public int Column;

        public CellType CellType { get; set; }
        
        public CellModel(int row, int column)
        {
            this.Row = row;
            this.Column = column;
            this.CellType = CellType.None;
        }
        
    }
}