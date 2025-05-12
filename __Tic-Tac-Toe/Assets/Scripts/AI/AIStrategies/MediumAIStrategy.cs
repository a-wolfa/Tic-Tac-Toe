using AI.AIStrategies.Abstractions;
using Managers;
using Model;
using UnityEngine;

public class MediumAIStrategy : IAIStrategy
{
    public Cell MakeMove(GameManager gameManager)
    {
        var availableMoves = gameManager.GetAvailableMoves();

        foreach (var move in availableMoves)
        {
            move.playedTurn = gameManager.CurrentPlayer;

            if (gameManager.CheckForWinner())
            {
                move.playedTurn = PlayerMove.None;
                return move;
            }

            move.playedTurn = PlayerMove.None;
        }

        foreach (var move in gameManager.GetAvailableMoves())
        {
            move.playedTurn = gameManager.CurrentPlayer == PlayerMove.X ? PlayerMove.O : PlayerMove.X;
            
            if (gameManager.CheckForWinner())
            {
                Debug.Log("There is win situation");
                move.playedTurn = PlayerMove.None;
                return move;
            }

            move.playedTurn = PlayerMove.None;
        }

        return new EasyAIStrategy().MakeMove(gameManager);
    }
}
