using System;
using Managers;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private Sprite xSprite;
        [SerializeField] private Sprite oSprite;

        private GameManager _gameManager;
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
            _gameManager = FindFirstObjectByType<GameManager>();
            _slot = GetComponent<Slot>();
        }

        private void InitCommands()
        {
            _button.onClick.AddListener(Move);
            _button.onClick.AddListener(DisableButton);
        }

        private void Move()
        {
            if (_gameManager.CheckForWinner())
                return;
            _button.image.sprite = _gameManager.CurrentTurn == Turn.O ? oSprite : xSprite;
            _slot.playedTurn = _gameManager.CurrentTurn;
            _gameManager.selectedSlot = _slot;
            _gameManager.onMoved.Invoke();
        }

        private void DisableButton()
        {
            _button.interactable = false;
        }
    }
}
