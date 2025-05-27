using AI.AIStrategies.Abstractions;
using Cell.Model;
using Managers;
using Model;

namespace AI.AIStrategies
{
    public class MediumAIStrategy : IAIStrategy
    {
        // public Model.Cell MakeMove(GameManager gameManager)
        // {
        //     var availableMoves = gameManager.GetAvailableMoves();
        //
        //     foreach (var move in availableMoves)
        //     {
        //         move.playedTurn = gameManager.CurrentPlayer;
        //
        //         // if (gameManager.Board.CheckForWinner())
        //         // {
        //         //     move.playedTurn = PlayerMove.None;
        //         //     return move;
        //         // }
        //
        //         move.playedTurn = PlayerMove.None;
        //     }
        //
        //     foreach (var move in gameManager.GetAvailableMoves())
        //     {
        //         move.playedTurn = gameManager.CurrentPlayer == PlayerMove.X ? PlayerMove.O : PlayerMove.X;
        //     
        //         // if (gameManager.Board.CheckForWinner())
        //         // {
        //         //     move.playedTurn = PlayerMove.None;
        //         //     return move;
        //         // }
        //
        //         move.playedTurn = PlayerMove.None;
        //     }
        //
        //     return new EasyAIStrategy().MakeMove(gameManager);
        // }
        public CellModel MakeMove(GameManager gameManager)
        {
            throw new System.NotImplementedException();
        }
    }
}
