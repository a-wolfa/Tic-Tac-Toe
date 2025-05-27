using AI.AIStrategies;
using AI.AIStrategies.Abstractions;
using Cell.Model;
using Managers;
using Model;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class AIPlayer
    {
        private readonly IAIStrategy _strategy;

        public AIPlayer(AIDifficulty difficulty)
        {
            _strategy = difficulty switch
            {
                AIDifficulty.Easy => new EasyAIStrategy(),
                AIDifficulty.Medium => new MediumAIStrategy(),
                AIDifficulty.Hard => new HardAIStrategy(),
                _ => new MediumAIStrategy()
            };
        }

        // public CellModel MakeMove(GameManager gameManager)
        // {
        //     return _strategy.MakeMove(gameManager);
        // }
    }
}