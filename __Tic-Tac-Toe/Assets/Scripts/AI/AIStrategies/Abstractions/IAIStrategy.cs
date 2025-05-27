using Cell.Model;
using Managers;
using Model;
using UnityEngine;

namespace AI.AIStrategies.Abstractions
{
    public interface IAIStrategy
    {
        CellModel MakeMove(GameManager gameManager);
    }
}