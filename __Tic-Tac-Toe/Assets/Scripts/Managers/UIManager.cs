using System;
using Events;
using Model;
using States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        private void OnGameOverTextUpdate(GameResult gameResult)
        {
            if (gameResult == GameResult.Draw)
            {
                UpdateStatus("It's a draw!");
            }
            else if (gameResult == GameResult.XWin)
            {
                UpdateStatus($"Player {PMove.X} wins!");
            }
            else if (gameResult == GameResult.OWin)
            {
                UpdateStatus($"Player {PMove.O} wins!");
            }
        }
    }
}