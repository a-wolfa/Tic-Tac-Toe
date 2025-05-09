using AI.AIStrategies.Abstractions;
using Managers;
using Model;
using UnityEngine;

public class MediumAIStrategy : IAIStrategy
{
    public Cell MakeMove(GameManager gameManager)
    {
        foreach (var move in gameManager.GetAvailableMoves())
        {
            move.playedTurn = gameManager.CurrentPlayer == PlayerMove.X ? PlayerMove.O : PlayerMove.X;

            if (gameManager.CheckForWinner())
            {
                move.playedTurn = PlayerMove.None;
                return move;
            }

            move.playedTurn = PlayerMove.None;
        }

        return new EasyAIStrategy().MakeMove(gameManager);
    }
}
