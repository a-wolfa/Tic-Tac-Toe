using System;
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
        [SerializeField] private GameManager gameManager;

        private void OnEnable()
        {
            gameManager.onGameOver.AddListener(OnGameOverTextUpdate);
        }

        private void OnDisable()
        {
            gameManager.onGameOver.RemoveListener(OnGameOverTextUpdate);
        }

        public Button resetButton;
        
        private void UpdateStatus(string message)
        {
            statusText.text = message;
        }   

        public void UpdateStatusText(PlayerMove currentPlayer)
        {
            if (currentPlayer == PlayerMove.None)
                return;
            UpdateStatus($"Player {currentPlayer}");
        }

        private void OnGameOverTextUpdate(bool isDraw)
        {
            if (isDraw)
            {
                UpdateStatus("It's a draw!");
            }
            else
            {
                UpdateStatus($"Player {gameManager.CurrentPlayer} wins!");
            }
        }
    }
}