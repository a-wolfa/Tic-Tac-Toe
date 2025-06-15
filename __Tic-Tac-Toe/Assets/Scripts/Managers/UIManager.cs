using System;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text statusText;
        
        public Button resetButton;

        private void UpdateStatus(string message)
        {
            statusText.text = message;
        }

        public void UpdateStatusText(PMove currentPlayer)
        {
            if (currentPlayer == PMove.None)
                return;
            UpdateStatus($"Player {currentPlayer}");
        }

        public void OnGameOverTextUpdate(GameResult gameResult)
        {
            UpdateStatus(gameResult == GameResult.Draw? "It's Draw" : $"{gameResult} Wins!");
        }
    }
}