using System;

namespace Core
{
    [Serializable]
    public class Cell : IDisposable
    {
        public int row;
        public int column;
        
        public PlayerType PlayerMarked { get; set; }

        public event Action CellMarked;
        
        public Cell(int row, int column)
        {
            this.row = row;
            this.column = column;
            PlayerMarked = PlayerType.None;
        }
        
        public void SetCell(PlayerType playerType)
        {
            PlayerMarked = playerType;
            CellMarked?.Invoke();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}