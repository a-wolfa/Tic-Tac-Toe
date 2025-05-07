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
            _button.onClick.AddListener(UpdateSlot);
        }

        private void UpdateSlot()
        {
            if (_gameManager.CurrentPlayer == Player.GameOver)
                return;
            
            Move();
            UpdateButtonSprite();
            DisableButton();
            
            _gameManager.onMoved.Invoke();
        }

        private void Move()
        {
            _cell.playedPlayer = _gameManager.CurrentPlayer;
            _gameManager.selectedCell = _cell;
        }

        private void UpdateButtonSprite()
        {
            _button.image.sprite = _gameManager.CurrentPlayer == Player.O ? oSprite : xSprite;
        }

        private void DisableButton()
        {
            _button.interactable = false;
        }
    }
}
