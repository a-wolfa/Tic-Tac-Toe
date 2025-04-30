using System;
using Model;
using States;
using States.Abstraction;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public UnityEvent onMoved;
        
        public Slot selectedSlot;
        public int moveCount = 0;
        
        public Turn CurrentTurn { get; set; }
        
        private Board _board;
        private Slot[,] _slots;
        
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
            _uiManager.UpdateStatus($"Player{CurrentTurn}'s turn!");
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
            _board = new Board();
            _slots = _board.GetBoard();
        }

        private void InitCommands()
        {
            onMoved.AddListener(UpdateGame);
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
                _uiManager.UpdateStatus($"{selectedSlot.playedTurn} won!");
            else if (moveCount >= SlotsCount)
                _uiManager.UpdateStatus("It's a draw!");
            else
                _uiManager.UpdateStatus($"Player{CurrentTurn}'s turn");
        }

        private void UpdateBoard()
        {
            var row = selectedSlot.row;
            var column = selectedSlot.column;
            _board.UpdateBoard(row, column, selectedSlot);
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
                    _slots[i, 0]?.playedTurn == _slots[i, 1]?.playedTurn && 
                    _slots[i, 1]?.playedTurn == _slots[i, 2]?.playedTurn || 
                    _slots[0, i] != null && 
                    _slots[0, i]?.playedTurn == _slots[1, i]?.playedTurn && 
                    _slots[1, i]?.playedTurn == _slots[2, i]?.playedTurn)
                {
                    return true;
                }
            }
            
            if (_slots[0,0] != null && 
                _slots[0,0]?.playedTurn == _slots[1,1]?.playedTurn && 
                _slots[1,1]?.playedTurn == _slots[2,2]?.playedTurn || 
                _slots[0,2] != null && 
                _slots[0,2]?.playedTurn == _slots[1,1]?.playedTurn && 
                _slots[1,1]?.playedTurn == _slots[2,0]?.playedTurn)
                return true;
            
            return false;
        }

    }
}
