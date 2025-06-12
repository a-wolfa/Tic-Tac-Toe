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
        public PMove CurrentMove { get; set; }

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
            _gameStateManager.SetState(new PlayerXTurnState(), this);
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
            UpdateGameState();
            UpdateMovesCount();
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
            _uiManager.UpdateStatusText(CurrentMove);
            lineRendererController.EraseLine();
        }

        private void ResetCells()
        {
            
        }

        public void NotifyGameOver(bool isDraw)
        {
            onGameOver.Invoke(isDraw);
        }

        
    }
}
