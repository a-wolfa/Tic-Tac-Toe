using Controllers;
using Line;
using Model;
using States;
using States.Abstraction;
using System.Collections;
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

        public UnityEvent WinnerFound;

        public AIDifficulty difficulty;

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

        [SerializeField] private ViewManager viewManager;
        [SerializeField] private LineRendererController lineRendererController;

        public Color playerXColor;
        public Color playerOColor;

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
            _slots = new Cell[BoardSize, BoardSize];
            _boardModel = new BoardModel(_slots);

            InitCommands();
            GetButtonBoard();
        }

        private void GetButtonBoard()
        {
            _buttons = new Button[9];
            for (int i = 0; i < panel.transform.childCount; i++)
            {
                Button button = panel.transform.GetChild(i).GetComponent<Button>();
                if (!button)
                    return;

                _buttons[i] = button;
                _slots[i / BoardSize, i % BoardSize] = button.GetComponent<Cell>();
                var cell = _slots[i / BoardSize, i % BoardSize];
                Debug.Log(cell.row + " " + cell.column);
            }
        }

        private void InitCommands()
        {
            onMoved.AddListener(UpdateGame);
            WinnerFound.AddListener(OnWinnerFound);
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
                _uiManager.UpdateStatus($"{selectedCell.playedTurn} Won!");
            else if (moveCount >= SlotsCount)
                _uiManager.UpdateStatus("Draw!");
            else
                _uiManager.UpdateStatus($"Player {CurrentPlayer}");
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
            return GetWinningCells() != null;
        }

        public List<Cell> GetWinningCells()
        {
            // Rows
            for (int i = 0; i < BoardSize; i++)
            {
                if (_slots[i, 0].playedTurn != PlayerMove.None &&
                    _slots[i, 0].playedTurn == _slots[i, 1].playedTurn &&
                    _slots[i, 1].playedTurn == _slots[i, 2].playedTurn)
                {
                    return new List<Cell> { _slots[i, 0], _slots[i, 1], _slots[i, 2] };
                }
            }

            // Columns
            for (int i = 0; i < BoardSize; i++)
            {
                if (_slots[0, i].playedTurn != PlayerMove.None &&
                    _slots[0, i].playedTurn == _slots[1, i].playedTurn &&
                    _slots[1, i].playedTurn == _slots[2, i].playedTurn)
                {
                    return new List<Cell> { _slots[0, i], _slots[1, i], _slots[2, i] };
                }
            }

            // Diagonal
            if (_slots[0, 0].playedTurn != PlayerMove.None &&
                _slots[0, 0].playedTurn == _slots[1, 1].playedTurn &&
                _slots[1, 1].playedTurn == _slots[2, 2].playedTurn)
            {
                return new List<Cell> { _slots[0, 0], _slots[1, 1], _slots[2, 2] };
            }

            // Anti-diagonal
            if (_slots[0, 2].playedTurn != PlayerMove.None &&
                _slots[0, 2].playedTurn == _slots[1, 1].playedTurn &&
                _slots[1, 1].playedTurn == _slots[2, 0].playedTurn)
            {
                return new List<Cell> { _slots[0, 2], _slots[1, 1], _slots[2, 0] };
            }

            return null;
        }



        private void ResetGame()
        {
            if (moveCount <= 0)
                return;

            moveCount = 0;

            ResetCells();
            selectedCell = null;
            SetState(new PlayerXTurnState());
            UpdateStatusText();
            lineRendererController.EraseLine();
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
            foreach (var cell in _slots)
            {
                Debug.Log(cell);
                if (cell.playedTurn == PlayerMove.None)
                {
                    availableMoves.Add(cell);
                }
            }

            return availableMoves;
        }

        private void MakeMove(Cell cell)
        {
            cell.GetComponent<ButtonController>().UpdateCell();
        }

        private IEnumerator DelayMove(Cell cell, float delaySeconds)
        {
            yield return new WaitForSeconds(delaySeconds);
            MakeMove(cell);
        }

        public void MakeMoveWithDelay(Cell cell, float delaySeconds)
        {
            UpdateStatusText();
            StartCoroutine(DelayMove(cell, delaySeconds));
        }

        private void OnWinnerFound()
        {
            var winningCells = GetWinningCells();
            Debug.Log(winningCells.Count);
            if (winningCells != null)
            {
                lineRendererController.SetCompleteLine(winningCells[0].transform.position, winningCells[2].transform.position);

                var color = CurrentPlayer.Equals(PlayerMove.X) ? playerXColor : playerOColor;
                lineRendererController.ColorLine(color);
            }

        }

        
    }
}
