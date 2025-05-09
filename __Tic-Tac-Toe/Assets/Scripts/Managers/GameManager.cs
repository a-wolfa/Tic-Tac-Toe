using Controllers;
using Model;
using States;
using States.Abstraction;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public PlayerType PlayerXType = PlayerType.Human;
        public PlayerType PlayerOType = PlayerType.AI;

        private BoardModel _boardModel;
        public UnityEvent onMoved;
        
        public Cell selectedCell;
        public int moveCount = 0;
        public GameObject panel;
        public PlayerMove CurrentPlayer { get; set; }
        
        private Cell[,] _slots;
        private Button[] _buttons;
        
        private const int BoardSize = 3;
        private const int SlotsCount = 9;
        
        private IGameState _currentState;
        
        [Inject] 
        private UIManager _uiManager;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            SetState(new PlayerXTurnState());
            _uiManager.UpdateStatus($"Player{CurrentPlayer}'s turn!");
        }

        public void SetState(IGameState newState)
        {
            _currentState?.ExitState(this);
            _currentState = newState;
            _currentState.EnterState(this);
        }

   
        private void Init()
        {
            InitCommands();
            GetButtonBoard();
            _boardModel = new BoardModel();
        }

        private void GetButtonBoard()
        {
            _buttons = new Button[9];
            for (int i = 0; i < panel.transform.childCount; i++)
            {
                if (panel.transform.GetChild(i).TryGetComponent<Button>(out var button))
                {
                    _buttons[i] = button;
                }
            }
        }

        private void InitCommands()
        {
            onMoved.AddListener(UpdateGame);
            _uiManager.resetButton.onClick.AddListener(ResetGame);
        }

        private void RemoveCommands()
        {
            onMoved.RemoveListener(UpdateGame);
            _uiManager.resetButton.onClick.RemoveListener(ResetGame);
        }
        
        private void OnDestroy()
        {
            RemoveCommands();
        }

        private void UpdateGame()
        {
            UpdateBoard();
            UpdateGameState();
            UpdateMovesCount();
            UpdateStatusText();
        }

        private void UpdateStatusText()
        {
            if (_currentState is GameOverState)
                _uiManager.UpdateStatus($"{selectedCell.playedTurn} won!");
            else if (moveCount >= SlotsCount)
                _uiManager.UpdateStatus("It's a draw!");
            else
                _uiManager.UpdateStatus($"Player{CurrentPlayer}'s turn");
        }

        private void UpdateBoard()
        {
            var row = selectedCell.row;
            var column = selectedCell.column;
            _boardModel.SetCell(row, column, selectedCell);
            _slots = _boardModel.GetBoard();
        }
        
        private void UpdateGameState()
        {
            _currentState.UpdateState(this);
        }

        private void UpdateMovesCount()
        {
            moveCount++;
        }

        public bool CheckForWinner()
        {
            Debug.Log(_slots[0, 0]);
            for (int i = 0; i < BoardSize; i++)
            {
                if (_slots[i, 0] != null && 
                    _slots[i, 0]?.playedTurn == _slots[i, 1]?.playedTurn && 
                    _slots[i, 1]?.playedTurn == _slots[i, 2]?.playedTurn || 
                    _slots[0, i] != null && 
                    _slots[0, i]?.playedTurn == _slots[1, i]?.playedTurn && 
                    _slots[1, i]?.playedTurn == _slots[2, i]?.playedTurn)
                {
                    return true;
                }
            }

            if (_slots[0, 0] != null &&
                _slots[0, 0]?.playedTurn == _slots[1, 1]?.playedTurn &&
                _slots[1, 1]?.playedTurn == _slots[2, 2]?.playedTurn ||
                _slots[0, 2] != null &&
                _slots[0, 2]?.playedTurn == _slots[1, 1]?.playedTurn &&
                _slots[1, 1]?.playedTurn == _slots[2, 0]?.playedTurn)
            {
                return true;
            }
            
            return false;
        }

        private void ResetGame()
        {
            if (moveCount <= 0)
                return;
            
            moveCount = 0;

            _boardModel.Reset();
            ResetCells();
            selectedCell = null;
            SetState(new PlayerXTurnState());
            UpdateStatusText();
        }

        private void ResetCells()
        {
            foreach (var button in _buttons)
            {
                button.interactable = true;
                button.image.sprite = null;
                var cell = button.GetComponent<Cell>();
                cell.playedTurn = PlayerMove.None;
            }
        }

        public List<Cell> GetAvailableMoves()
        {
            List<Cell> availableMoves = new List<Cell>();
            foreach (var cell in FindObjectsByType<Cell>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
            {
                if (cell.playedTurn == PlayerMove.None)
                {
                    availableMoves.Add(cell);
                }
            }

            return availableMoves;
        }

        public void MakeMove(Cell cell)
        {
            cell.GetComponent<ButtonController>().UpdateCell();
        }

    }
}
