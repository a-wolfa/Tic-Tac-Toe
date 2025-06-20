using System;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text statusText;

        public Button resetButton;
        public UnityEvent reset;

        private void Awake()
        {
            InitCommands();
        }

        private void InitCommands()
        {
            resetButton.onClick.AddListener(OnReset);
        }
        
        private void RemoveCommands()
        {
            resetButton.onClick.RemoveAllListeners();
        }

        private void OnDestroy()
        {
            RemoveCommands();
        }

        private void OnReset()
        {
            reset.Invoke();
        }

        public void UpdateStatusText(GameResult gameResult, PMove currentPlayer)
        {
            if (gameResult == GameResult.InProgress)
            {
                statusText.text = $"Player {currentPlayer}";
            }
            else if (gameResult == GameResult.Draw)
            {
                statusText.text = "It's a Draw";
            }
            else
            {
                statusText.text = $"{gameResult} Wins!";
            }
        }
        
        
    }
}