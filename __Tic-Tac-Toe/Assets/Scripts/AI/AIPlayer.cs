using AI.AIStrategies.Abstractions;
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

        public Cell MakeMove(GameManager gameManager)
        {
            return _strategy.MakeMove(gameManager);
        }
    }
}