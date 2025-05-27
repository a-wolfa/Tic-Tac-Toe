using Board.Model;
using Board.View;
using Model;

namespace Board.Controllers
{
    public class BoardController
    {
        private BoardModel _boardModel;
        private BoardView _boardView;

        public void Init(BoardModel boardModel, BoardView boardView)
        {
            _boardModel = boardModel;
            _boardView = boardView;
        }

        public void PlaceMark(int row, int column, PlayerMove mark)
        {
            // TODO
        }
    }
}