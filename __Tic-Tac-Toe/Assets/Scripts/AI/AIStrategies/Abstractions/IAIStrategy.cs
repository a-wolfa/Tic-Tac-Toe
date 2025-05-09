using Managers;
using Model;
using UnityEngine;

namespace AI.AIStrategies.Abstractions
{
    public interface IAIStrategy
    {
        Cell MakeMove(GameManager gameManager);
    }
}