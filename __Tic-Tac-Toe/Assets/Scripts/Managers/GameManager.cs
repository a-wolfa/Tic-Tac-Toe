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
using Unity.VisualScripting;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private ViewManager viewManager;
        [SerializeField] private LineRendererController lineRendererController;
        [SerializeField] private BoardController board;

        public GameStateManager gameStateManager;

        [Header("Player Settings")]
        public PlayerType playerXType = PlayerType.Human;
        public PlayerType playerOType = PlayerType.AI;
        public AIDifficulty difficulty = AIDifficulty.Medium;

        [Header("Game Flow")]
        public GameResult gameResult;
        public PMove CurrentMove;

        public Color playerXColor;
        public Color playerOColor;

        public int moveCount;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            gameResult = GameResult.InProgress;

            InitComponents();
            InitCommands();
        }

        private void InitComponents()
        {
            gameStateManager = new GameStateManager();
        }

        private void InitCommands()
        {
            _uiManager.reset.AddListener(Reset);
        }
        
        private void Start()
        {
            gameStateManager.SetState(new PlayerXTurnState(), this);
        }

        private void RemoveCommands()
        {
            _uiManager.reset.RemoveAllListeners();
        }

        private void OnDestroy() => RemoveCommands();

        public void UpdateGame()
        {
            UpdateMovesCount();
            UpdateGameState();
        }

        private void UpdateGameState()
        {
            gameStateManager.UpdateState(this);
        }

        private void UpdateMovesCount()
        {
            moveCount++;
        }

        private void Reset()
        {
            gameResult = GameResult.InProgress;
            moveCount = 0;
            gameStateManager.SetState(new PlayerXTurnState(), this);
            board.Reset();
            UpdateStatus();
        }

        public void UpdateStatus()
        {
            _uiManager.UpdateStatusText(gameResult,CurrentMove);
        }

        public PMove CheckWin()
        {
            return board.GetModel().CheckWin();
        }
    }
}
