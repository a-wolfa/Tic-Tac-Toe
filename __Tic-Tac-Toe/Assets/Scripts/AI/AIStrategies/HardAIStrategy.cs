using AI.AIStrategies.Abstractions;
using Managers;
using Model;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HardAIStrategy : IAIStrategy
{
    public Cell MakeMove(GameManager gameManager)
    {
        if (gameManager.moveCount < 2)
            return new EasyAIStrategy().MakeMove(gameManager);

        var bestMove = MiniMax(gameManager.GetAvailableMoves(), true, gameManager);
        return bestMove.cell;
    }

    private (Cell cell, int score) MiniMax(List<Cell> availableMoves, bool isMaximizing, GameManager gameManager)
    {
        if (gameManager.CheckForWinner())
        {
            return (null, isMaximizing ? -1 : 1);
        }

        if (availableMoves.Count == 0)
        {
            return (null, 0);
        }

        Cell bestCell = null;
        int bestScore = isMaximizing ? int.MinValue : int.MaxValue;

        foreach (Cell move in availableMoves)
        {
            move.playedTurn = isMaximizing ? gameManager.CurrentPlayer :
                (gameManager.CurrentPlayer == PlayerMove.X ? PlayerMove.O : PlayerMove.X);

            var (_, score) = MiniMax(gameManager.GetAvailableMoves(), !isMaximizing, gameManager);

            move.playedTurn = PlayerMove.None;

            if (isMaximizing)
            {
                if (score > bestScore)
                {
                    bestScore = score;
                    bestCell = move;
                }
            }
            else
            {
                if (score < bestScore)
                {
                    bestScore = score;
                    bestCell = move;
                }
            }
        }

        return (bestCell, bestScore);
    }
}
