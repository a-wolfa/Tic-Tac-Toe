using Line;
using Model;
using States;
using States.Abstraction;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AI;
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

        [Inject] private UIManager _uiManager;
        public GameStateManager GameStateManager;
        [SerializeField] private ViewManager viewManager;
        [SerializeField] private LineRendererController lineRendererController;

        public Color playerXColor;
        public Color playerOColor;

        public BoardController board;

        private void Awake()
        {
            Init();
        }
        
        [Inject]
        public void Construct(GameStateManager gameStateManager)
        {
            GameStateManager = gameStateManager;
        }

        private void Init()
        {
            // TODO

            InitCommands();
            
        }

        private void InitCommands()
        {
            onMoved.AddListener(UpdateGame);
//            _uiManager.resetButton.onClick.AddListener(ResetGame);
        }
        
        private void Start()
        {
//            _uiManager.UpdateStatusText(CurrentPlayer);
            GameStateManager.SetState(GameStateManager.XTurnState, this);
        }
        
        private void RemoveCommands()
        {
            
        }

        private void OnDestroy() => RemoveCommands();

        private void UpdateGame()
        {
            UpdateGameState();
            UpdateMovesCount();
        }

        private void UpdateGameState()
        {
            GameStateManager.UpdateState(this);
        }

        private void UpdateMovesCount() => moveCount++;
        

        private void ResetCells()
        {
            
        }

        public void NotifyGameOver(bool isDraw)
        {
            onGameOver.Invoke(isDraw);
        }

        
    }
}
