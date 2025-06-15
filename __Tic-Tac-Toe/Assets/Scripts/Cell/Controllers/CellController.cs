using UnityEngine;
using Cell.Model;
using System;
using UnityEngine.UI;
using Cell.View;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Model;
using Managers;
using Unity.VisualScripting;
using Zenject;

namespace Cell.Controllers
{
    public class CellController : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private CellModel _model = new(1, 1);
        
        [Inject] private GameManager _gameManager;
        
        private CellView _view;

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
            _view = GetComponent<CellView>();
        }

        private void InitCommands()
        {
            _model.OnCellChanged += (pMove) =>
            {
                if (_gameManager.gameResult != GameResult.InProgress)
                    return;
                
                _view.UpdateCell(pMove);
            };
            
            _model.OnCellChanged += (pMove) =>
            {
                if (_gameManager.gameResult != GameResult.InProgress)
                    return;
                
                _gameManager.onMoved.Invoke();
            };
        }

        private void SelectCell()
        {
            _model.HandleCellChanged(FindAnyObjectByType<GameManager>().CurrentMove);
        }

        private void OnValidate()
        {
            name = $"Cell ({_model.Row}, {_model.Column})";
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_model.Move != PMove.None)
                return;

            SelectCell();
        }

        public CellModel GetModel()
        {
            return _model;
        }
    }
}
