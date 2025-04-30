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
        private Slot _slot;

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
            _slot = GetComponent<Slot>();
        }

        private void InitCommands()
        {
            _button.onClick.AddListener(UpdateSlot);
        }

        private void UpdateSlot()
        {
            if (_gameManager.CurrentTurn == Turn.GameOver)
                return;
            
            Move();
            UpdateButtonSprite();
            DisableButton();
            
            _gameManager.onMoved.Invoke();
        }

        private void Move()
        {
            _slot.playedTurn = _gameManager.CurrentTurn;
            _gameManager.selectedSlot = _slot;
        }

        private void UpdateButtonSprite()
        {
            _button.image.sprite = _gameManager.CurrentTurn == Turn.O ? oSprite : xSprite;
        }

        private void DisableButton()
        {
            _button.interactable = false;
        }
    }
}
