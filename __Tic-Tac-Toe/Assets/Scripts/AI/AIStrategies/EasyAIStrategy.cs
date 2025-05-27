using AI.AIStrategies.Abstractions;
using Cell.Model;
using Managers;

namespace AI.AIStrategies
{
    public class EasyAIStrategy : IAIStrategy
    {
        // public Cell MakeMove(GameManager gameManager)
        // {
        //     var availableMoves = gameManager.GetAvailableMoves();
        //     if (availableMoves.Count > 0)
        //     {
        //         int randomIndex = Random.Range(0, availableMoves.Count);
        //         return availableMoves[randomIndex];
        //     }
        //
        //     return null;
        // }

        public CellModel MakeMove(GameManager gameManager)
        {
            throw new System.NotImplementedException();
        }
    }
}
