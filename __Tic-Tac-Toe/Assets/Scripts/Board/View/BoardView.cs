using Board.Abstractions;
using Model;
using UnityEngine;

namespace Board.View
{
    public class BoardView : MonoBehaviour, IBoardView
    {
        [SerializeField] private Cell[,] _board;
        
        public void UpdateBoard(int row, int column, PlayerMove move)
        {
            _board[row, column].playedTurn = move;
        }
    }
}
