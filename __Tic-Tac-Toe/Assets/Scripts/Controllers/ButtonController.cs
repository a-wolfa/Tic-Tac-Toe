using System;
using Managers;
using Model;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controllers
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private Sprite xSprite;
        [SerializeField] private Sprite oSprite;

        [Inject] private GameManager _gameManager;
        private Button _button; 
        private Cell _cell;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            InitComponents();
            InitCommands();
        }

        private void InitComponents()
        {
            _button = GetComponent<Button>();
            _cell = GetComponent<Cell>();
        }

        private void InitCommands()
        {
            _button.onClick.AddListener(UpdateCell);
        }

        public void UpdateCell()
        {
            if (_gameManager.CurrentPlayer == PlayerMove.None)
                return;
            
            Move();
            UpdateButtonSprite();
            DisableButton();
            
            _gameManager.onMoved.Invoke();
        }

        private void Move()
        {
            _cell.playedTurn = _gameManager.CurrentPlayer;
            _gameManager.selectedCell = _cell;
        }

        private void UpdateButtonSprite()
        {
            _button.image.sprite = _gameManager.CurrentPlayer == PlayerMove.O ? oSprite : xSprite;
        }

        private void DisableButton()
        {
            _button.interactable = false;
        }
    }
}
