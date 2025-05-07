using System;
using Model;
using UnityEngine;
using UnityEngine.UI;
using View.Abstractions;

namespace View
{
    public class BoardView : MonoBehaviour, IBoardView
    {
        public Button[,] Buttons = new Button[3, 3];
        public Button resetButton;

        public event Action<int, int> CellClicked;
        public event Action ResetClicked;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            InitCommands();
        }

        private void InitCommands()
        {
            for (int row = 0; row < 3; row++)
            for (int col = 0; col < 3; col++)
            {
                int r = row, c = col;
                Buttons[row, col].onClick.AddListener(() => CellClicked?.Invoke(r, c));
            }
            
            resetButton.onClick.AddListener(() => ResetClicked?.Invoke());
        }

        public void UpdateCell(int row, int column)
        {
            throw new System.NotImplementedException();
        }

        public void ShowWinner(Player winner)
        {
            throw new System.NotImplementedException();
        }

        public void ResetBoard()
        {
            throw new System.NotImplementedException();
        }
    }
}