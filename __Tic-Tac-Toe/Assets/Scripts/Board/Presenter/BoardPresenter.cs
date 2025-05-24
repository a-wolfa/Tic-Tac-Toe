using System.Collections.Generic;
using Board.Abstractions;
using Board.Model;
using Model;
using UnityEngine;

namespace Board.Presenter
{
    public class BoardPresenter : MonoBehaviour
    {
        private readonly BoardModel _boardModel;
        private readonly IBoardView _boardView;

        public BoardPresenter(BoardModel boardModel, IBoardView boardView)
        {
            _boardModel = boardModel;
            _boardView = boardView;
        }

        public void SetCell(int row, int column, PlayerMove move)
        {
            var cell = _boardModel.GetCell(row, column);

            if (cell.playedTurn != PlayerMove.None)
            {
                return;
            }

            cell.playedTurn = move;
            _boardModel.SetCell(row, column, cell);
            _boardView.UpdateBoard(row, column, move);
        }

        public void ResetBoard()
        {
            var board = _boardModel.GetBoard();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j].playedTurn = PlayerMove.None;
                    _boardView.UpdateBoard(j, j, PlayerMove.None);
                }
            }

            _boardModel.SetBoard(board);
        }

        public List<Cell> GetWinningCells()
        {
            var board = _boardModel.GetBoard();
            var winningCells = new List<Cell>();


            // Rows
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0].playedTurn != PlayerMove.None &&
                    board[i, 0].playedTurn == board[i, 1].playedTurn &&
                    board[i, 1].playedTurn == board[i, 2].playedTurn)
                {
                    return new List<Cell> { board[i, 0], board[i, 1], board[i, 2] };
                }
            }

            // Columns
            for (int i = 0; i < 3; i++)
            {
                if (board[0, i].playedTurn != PlayerMove.None &&
                    board[0, i].playedTurn == board[1, i].playedTurn &&
                    board[1, i].playedTurn == board[2, i].playedTurn)
                {
                    return new List<Cell> { board[0, i], board[1, i], board[2, i] };
                }
            }

            // Diagonal
            if (board[0, 0].playedTurn != PlayerMove.None &&
                board[0, 0].playedTurn == board[1, 1].playedTurn &&
                board[1, 1].playedTurn == board[2, 2].playedTurn)
            {
                return new List<Cell> { board[0, 0], board[1, 1], board[2, 2] };
            }

            // Anti-diagonal
            if (board[0, 2].playedTurn != PlayerMove.None &&
                board[0, 2].playedTurn == board[1, 1].playedTurn &&
                board[1, 1].playedTurn == board[2, 0].playedTurn)
            {
                return new List<Cell> { board[0, 2], board[1, 1], board[2, 0] };
            }

            return null;
        }
        
        public bool CheckForWinner()
        {
            return GetWinningCells() != null;
        }
    }
}