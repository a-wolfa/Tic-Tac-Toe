using Controllers;
using Line;
using Model;
using States;
using States.Abstraction;
using System.Collections;
using System.Collections.Generic;
using Board;
using Board.Model;
using Board.Presenter;
using Board.View;
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

        public BoardPresenter Board;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _slots = new Cell[BoardSize, BoardSize];
            _boardModel = new BoardModel(_slots);
            
            Board = new BoardPresenter(_boardModel, new BoardView());

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

        public void NotifyGameOver(bool isDraw)
        {
            onGameOver.Invoke(isDraw);
        }

        
    }
}
