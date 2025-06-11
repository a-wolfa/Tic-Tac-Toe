using System;
using UnityEngine;
using Cell.Controllers;
using UnityEngine.UI;

namespace Cell.View
{
    public class CellView : MonoBehaviour
    {
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
            if (_button == null)
            {
                Debug.LogError("Button component not found on CellView.");
            }
        }

        private void InitCommands()
        {
            _button?.onClick.AddListener(OnCellClicked);
        }

        private void RemoveCommands()
        {
            _button?.onClick.RemoveListener(OnCellClicked);
        }
        

        private void OnCellClicked()
        {
            Debug.Log("Button clicked: " + gameObject.name);
        }
    }
}

