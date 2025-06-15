using System;
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

        public UnityEvent<GameResult> onGameOver;
        public UnityEvent onMoved;
        
        public int moveCount;
        public PMove CurrentMove { get; set; }

        [Inject] private UIManager _uiManager;
        public GameStateManager GameStateManager;
        [SerializeField] private ViewManager viewManager;
        [SerializeField] private LineRendererController lineRendererController;

        public Color playerXColor;
        public Color playerOColor;

        public BoardController board;
        public GameResult gameResult;

        private void Awake()
        {
            Init();
        }
        
        [Inject]
        public void Construct(
            GameStateManager gameStateManager,
            BoardController boardController
            )
        {
            GameStateManager = gameStateManager;
            board = boardController;
        }

        private void Init()
        {
            // TODO
            
            gameResult = GameResult.InProgress;
            InitCommands();
        }

        private void InitCommands()
        {
            onGameOver.AddListener(_uiManager.OnGameOverTextUpdate);

            onMoved.AddListener(UpdateGame);
            onMoved.AddListener(() =>
            {
                Debug.Log(gameResult);
                if (gameResult != GameResult.InProgress)
                    return;
                _uiManager.UpdateStatusText(CurrentMove);
            });
            
            _uiManager.resetButton.onClick.AddListener(Reset);
        }
        
        private void Start()
        {
//            _uiManager.UpdateStatusText(CurrentPlayer);
            GameStateManager.SetState(GameStateManager.XTurnState, this);
        }
        
        private void RemoveCommands()
        {
            onMoved.RemoveAllListeners();
            
            onGameOver.RemoveAllListeners();
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
        

        public void NotifyGameOver(GameResult gameResult)
        {
            onGameOver.Invoke(gameResult);
        }

        private void Reset()
        {
            gameResult = GameResult.InProgress;
            GameStateManager.SetState(GameStateManager.XTurnState, this);
            moveCount = 0;
        }
    }
}
