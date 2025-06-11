using Line;
using Model;
using States;
using States.Abstraction;
using System.Collections;
using System.Collections.Generic;
using Board.Controllers;
using Board.Model;
using Cell.Controllers;
using Cell.Model;
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

        public CellController selectedCell;
        public int moveCount;
        public GameObject panel;
        public PlayerMove CurrentPlayer { get; set; }

        private CellModel[,] _cellModels;
        private CellController[] _cellControllers;

        private const int BoardSize = 3;

        public IGameState CurrentGameState;

        [Inject] private UIManager _uiManager;
        [Inject] private GameStateManager _gameStateManager;
        [SerializeField] private ViewManager viewManager;
        [SerializeField] private LineRendererController lineRendererController;

        public Color playerXColor;
        public Color playerOColor;

        public BoardController Board;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            // TODO

            InitCommands();
            // GetButtonBoard();
        }

        private void InitCommands()
        {
            onMoved.AddListener(UpdateGame);
//            _uiManager.resetButton.onClick.AddListener(ResetGame);
        }
        
        private void Start()
        {
//            _gameStateManager.SetState(new PlayerXTurnState(), this);
//            _uiManager.UpdateStatusText(CurrentPlayer);
        }
        
        // private void GetButtonBoard()
        // {
        //     _buttons = new Button[9];
        //     for (int i = 0; i < panel.transform.childCount; i++)
        //     {
        //         Button button = panel.transform.GetChild(i).GetComponent<Button>();
        //         if (!button)
        //             return;
        //
        //         _buttons[i] = button;
        //         _slots[i / BoardSize, i % BoardSize] = button.GetComponent<Cell>();
        //     }
        // }
        
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
            // TODO
        }

        private void UpdateGameState()
        {
            _gameStateManager.UpdateState(this);
        }

        private void UpdateMovesCount() => moveCount++;

        private void ResetGame()
        {
            if (moveCount <= 0) return;
            moveCount = 0;
            ResetCells();
            selectedCell = null;
            _gameStateManager.SetState(new PlayerXTurnState(), this);
            _uiManager.UpdateStatusText(CurrentPlayer);
            lineRendererController.EraseLine();
        }

        private void ResetCells()
        {
            // foreach (var button in _buttons)
            // {
            //     button.interactable = true;
            //     button.image.sprite = null;
            //     var cell = button.GetComponent<Cell>();
            //     cell.playedTurn = PlayerMove.None;
            // }
        }

        // public List<CellModel> GetAvailableMoves()
        // {
        //     // var availableMoves = new List<Cell>();
        //     // foreach (var cell in _slots)
        //     // {
        //     //     if (cell.playedTurn == PlayerMove.None)
        //     //     {
        //     //         availableMoves.Add(cell);
        //     //     }
        //     // }
        //     //
        //     // return availableMoves;
        // }

        // private void MakeMove(Cell cell)
        // {
        //     cell.GetComponent<CellController>().UpdateCell();
        // }

        // private IEnumerator DelayMove(Cell cell, float delaySeconds)
        // {
        //     yield return new WaitForSeconds(delaySeconds);
        //     MakeMove(cell);
        // }

        // public void MakeMoveWithDelay(Cell cell, float delaySeconds)
        // {
        //     _uiManager.UpdateStatusText(CurrentPlayer);
        //     StartCoroutine(DelayMove(cell, delaySeconds));
        // }

        public void NotifyGameOver(bool isDraw)
        {
            onGameOver.Invoke(isDraw);
        }

        
    }
}
