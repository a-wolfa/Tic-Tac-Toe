using AI.AIStrategies.Abstractions;
using Managers;
using Model;
using UnityEngine;

public class AIPlayer
{
    private readonly IAIStrategy strategy;

    public AIPlayer(AIDifficulty difficulty)
    {
        strategy = difficulty switch
        {
            AIDifficulty.Easy => new EasyAIStrategy(),
            AIDifficulty.Medium => new MediumAIStrategy(),
            AIDifficulty.Hard => new HardAIStrategy(),
            _ => throw new System.ArgumentOutOfRangeException(nameof(difficulty), difficulty, null)
        };
    }

    public Cell MakeMove(GameManager gameManager)
    {
        return strategy.MakeMove(gameManager);
    }
}
