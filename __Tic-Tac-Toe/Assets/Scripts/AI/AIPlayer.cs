using Managers;
using Model;
using UnityEngine;

public class AIPlayer
{
    [SerializeField] private GameManager gameManager;

    public AIPlayer(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public Cell MakeMove()
    {
        var availableMoves = gameManager.GetAvailableMoves();
        if (availableMoves.Count > 0)
        {
            int randomIndex = Random.Range(0, availableMoves.Count);
            return availableMoves[randomIndex];
        }

        return null;
    }
}
