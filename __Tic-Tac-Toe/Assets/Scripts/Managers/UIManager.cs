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

        private void OnEnable()
        {
//            GameEvents.GameOver.AddListener(OnGameOverTextUpdate);
        }

        private void OnDisable()
        {
            GameEvents.GameOver.RemoveListener(OnGameOverTextUpdate);
        }

        public Button resetButton;
        
        private void UpdateStatus(string message)
        {
            statusText.text = message;
        }   

        public void UpdateStatusText(PlayerMove currentPlayer)
        {
            if (currentPlayer == PlayerMove.GameOver)
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
                UpdateStatus($"Player {PlayerMove.X} wins!");
            }
            else if (gameResult == GameResult.OWin)
            {
                UpdateStatus($"Player {PlayerMove.O} wins!");
            }
        }
    }
}