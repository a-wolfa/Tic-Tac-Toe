using Model;

namespace Board.Abstractions
{
    public interface IBoardView
    {
        void UpdateBoard(int row, int column, PlayerMove player);
    }
}