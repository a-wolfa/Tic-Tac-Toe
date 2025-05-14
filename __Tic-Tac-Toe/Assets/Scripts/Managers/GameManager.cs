using Controllers;
using Line;
using Model;
using States;
using States.Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public PlayerType playerXType = PlayerType.Human;
        public PlayerType playerOType = PlayerType.AI;
        public AIDifficulty difficulty = AIDifficulty.Medium;

        public UnityEvent<bool> onGameOver;
        public UnityEvent onMoved;

        private BoardModel _boardModel;

        public Cell selectedCell;
        public int moveCount;
        public GameObject panel;
        public PlayerMove CurrentPlayer { get; set; }

        private Cell[,] _slots;
        private Button[] _buttons;

        private const int BoardSize = 3;

        public IGameState CurrentGameState;

        [Inject] private UIManager _uiManager;
        [SerializeField] private ViewManager viewManager;
        [SerializeField] private LineRendererController lineRendererController;

        public Color playerXColor;
        public Color playerOColor;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _slots = new Cell[BoardSize, BoardSize];
            _boardModel = new BoardModel(_slots);

            InitCommands();
            GetButtonBoard();
        }

        private void InitCommands()
        {
            onMoved.AddListener(UpdateGame);
            _uiManager.resetButton.onClick.AddListener(ResetGame);
        }
        
        private void Start()
        {
            SetState(new PlayerXTurnState());
            _uiManager.UpdateStatusText(CurrentPlayer);
        }

        public void SetState(IGameState newState)
        {
            CurrentGameState?.ExitState(this);
            CurrentGameState = newState;
            CurrentGameState.EnterState(this);
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
            }
        }
        
        private void RemoveCommands()
        {
            onMoved.RemoveListener(UpdateGame);
            _uiManager.resetButton.onClick.RemoveListener(ResetGame);
        }

        private void OnDestroy() => RemoveCommands();

        private void UpdateGame()
        {
            UpdateBoard();
            UpdateGameState();
            UpdateMovesCount();
        }
        
        private void UpdateBoard()
        {
            var row = selectedCell.row;
            var column = selectedCell.column;
            _boardModel.SetCell(row, column, selectedCell);
            _slots = _boardModel.GetBoard();
        }

        private void UpdateGameState() => CurrentGameState.UpdateState(this);

        private void UpdateMovesCount() => moveCount++;

        public bool CheckForWinner() => GetWinningCells() != null;

        private List<Cell> GetWinningCells()
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
            if (moveCount <= 0) return;
            moveCount = 0;
            ResetCells();
            selectedCell = null;
            SetState(new PlayerXTurnState());
            _uiManager.UpdateStatusText(CurrentPlayer);
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
            var availableMoves = new List<Cell>();
            foreach (var cell in _slots)
            {
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
            _uiManager.UpdateStatusText(CurrentPlayer);
            StartCoroutine(DelayMove(cell, delaySeconds));
        }

        private void OnWinnerFound()
        { 
            // TODO
            
            // send this to LineRenderer controller
            // var winningCells = GetWinningCells();
            // if (winningCells.Count == 3)
            // {
            //     lineRendererController.SetCompleteLine(
            //         winningCells[0].transform.position, 
            //         winningCells[2].transform.position);
            //
            //     var color = CurrentPlayer.Equals(PlayerMove.X) ? playerXColor : playerOColor;
            //     lineRendererController.ColorLine(color);
            // }
        }

        public void NotifyGameOver(bool isDraw)
        {
            onGameOver.Invoke(isDraw);
        }

        
    }
}
