using Model;

namespace View.Abstractions
{
    public interface IBoardView
    {
        void UpdateCell(int row, int column);
        void ShowWinner(PlayerMove winner);
        void ResetBoard();
    }
}