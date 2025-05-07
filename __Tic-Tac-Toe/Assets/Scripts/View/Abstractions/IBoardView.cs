using Model;

namespace View.Abstractions
{
    public interface IBoardView
    {
        void UpdateCell(int row, int column);
        void ShowWinner(Player winner);
        void ResetBoard();
    }
}