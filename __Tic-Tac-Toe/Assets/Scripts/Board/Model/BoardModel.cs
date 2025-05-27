using Cell.Model;

namespace Board.Model
{
    public class BoardModel
    {
        private CellModel[,] _cellModels;

        public BoardModel(CellModel[,] cellModels)
        {
            _cellModels = cellModels;
        }

        public void SetCell(int row, int column, CellModel selectedCell)
        {
            _cellModels[row, column] = selectedCell;
        }
        
        public CellModel[,] GetBoard()
        {
            return _cellModels;
        }
    }
}
