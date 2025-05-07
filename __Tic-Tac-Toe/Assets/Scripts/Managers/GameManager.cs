using Model;
using States;
using States.Abstraction;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        
        private BoardModel _boardModel;
        public UnityEvent onMoved;
        
        public Cell selectedCell;
        public int moveCount = 0;
        public GameObject panel;
        public Player CurrentPlayer { get; set; }
        
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
            _boardModel.SetBoard(_slots);
        }

        private void GetButtonBoard()
        {
            _buttons = new Button[9];
            _slots = new Cell[BoardSize, BoardSize];
            for (int i = 0; i < panel.transform.childCount; i++)
            {
                if (panel.transform.GetChild(i).TryGetComponent<Button>(out var button))
                {
                    _buttons[i] = button;
                    _slots[i / BoardSize, i % BoardSize] = button.GetComponent<Cell>();
                    Debug.Log(_slots[i / BoardSize, i % BoardSize].playedPlayer);
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
                _uiManager.UpdateStatus($"{selectedCell.playedPlayer} won!");
            else if (moveCount >= SlotsCount)
                _uiManager.UpdateStatus("It's a draw!");
            else
                _uiManager.UpdateStatus($"Player{CurrentPlayer}'s turn");
        }

        private void UpdateBoard()
        {
            Debug.Log(selectedCell.playedPlayer);
            var row = selectedCell.row;
            var column = selectedCell.column;
            _boardModel.SetCell(row, column, selectedCell);
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
            for (int i = 0; i < BoardSize; i++)
            {
                if (_slots[i, 0] != null && 
                    _slots[i, 0]?.playedPlayer == _slots[i, 1]?.playedPlayer && 
                    _slots[i, 1]?.playedPlayer == _slots[i, 2]?.playedPlayer || 
                    _slots[0, i] != null && 
                    _slots[0, i]?.playedPlayer == _slots[1, i]?.playedPlayer && 
                    _slots[1, i]?.playedPlayer == _slots[2, i]?.playedPlayer)
                {
                    return true;
                }
            }
            
            if (_slots[0,0] != null && 
                _slots[0,0]?.playedPlayer == _slots[1,1]?.playedPlayer && 
                _slots[1,1]?.playedPlayer == _slots[2,2]?.playedPlayer || 
                _slots[0,2] != null && 
                _slots[0,2]?.playedPlayer == _slots[1,1]?.playedPlayer && 
                _slots[1,1]?.playedPlayer == _slots[2,0]?.playedPlayer)
                return true;
            
            return false;
        }

        private void ResetGame()
        {
            _boardModel.Reset();
            ResetButtons();
            moveCount = 0;
            ResetCells();
            UpdateStatusText();
            _currentState = new PlayerXTurnState();
            
        }

        private void ResetButtons()
        {
            foreach (var button in _buttons)
            {
                button.interactable = true;
                button.image.sprite = null;
            }
        }

        private void ResetCells()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    Debug.Log(_slots[i, j].playedPlayer);
                    _slots[i, j].playedPlayer = Player.GameOver;
                }
            }
        }

    }
}
