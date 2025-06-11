using UnityEngine;
using Cell.Model;
using System;
using UnityEngine.UI;
using Cell.View;
using UnityEngine.Events;

namespace Cell.Controllers
{
    public class CellController : MonoBehaviour
    {
        [SerializeField] private CellModel _model = new(1, 1);
        
        private CellView _view;
        private Button _cellButton;

        public static UnityEvent<int, int> OnCellSelected;

        private void Awake()
        {
            Init();    
        }

        private void Init()
        {
            InitComponents();
            InitCommands();
        }

        private void InitCommands()
        {
            _cellButton?.onClick.AddListener(SelectCell);
        }

        private void InitComponents()
        {
            _cellButton = GetComponent<Button>();
            _view = GetComponent<CellView>();
        }


        private void SelectCell()
        {
            Debug.Log($"Cell selected: {_model.Row}, {_model.Column}");
            OnCellSelected?.Invoke(_model.Row, _model.Column);
        }

        private void OnValidate()
        {
            name = $"Cell ({_model.Row}, {_model.Column})";
        }
    }
}
