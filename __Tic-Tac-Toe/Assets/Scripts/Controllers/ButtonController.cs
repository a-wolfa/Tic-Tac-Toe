using System;
using Managers;
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
        }

        private void InitCommands()
        {
            _button.onClick.AddListener(Move);
        }

        private void Move()
        {
            _button.image.sprite = _gameManager.CurrentTurn == GameManager.Turn.O ? oSprite : xSprite;
            _gameManager.onMoved.Invoke();
        }
    }
}
