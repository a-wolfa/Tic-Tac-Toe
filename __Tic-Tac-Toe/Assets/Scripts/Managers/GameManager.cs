using States;
using Core;
using UnityEngine;
using View;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField] private UIManager uiManager;
        [SerializeField] private ViewManager viewManager;
        [SerializeField] private CellView[] cellViews;

        public GameStateManager GameStateManager;
        public Board board;
        private int _moveCount = 0;

        [Header("Game Flow")]
        public PlayerType result;
        public PlayerType currentPlayer;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            InitComponents();
            InitBoard();
        }

        private void InitBoard()
        {
            board = new Board();
            for (int iterator = 0; iterator < 9; iterator++)
            {
                var i = iterator / 3;
                var j = iterator % 3;
                board[i, j] = cellViews[iterator].Cell;
            }
        }

        private void InitComponents()
        {
            currentPlayer = PlayerType.None;
            GameStateManager = new GameStateManager();
        }
        
        private void Start()
        {
            GameStateManager.SetState(new XTurnState(), this);
        }

        private void RemoveCommands()
        {
            uiManager.reset.RemoveAllListeners();
        }

        private void OnDestroy() => RemoveCommands();

        public void UpdateStatus(string text)
        {
            uiManager.UpdateStatusText(text);
        }

        public void SetActivePlayer(PlayerType player)
        {
            currentPlayer = player;
        }

        public PlayerType GetActivePlayer()
        {
            return currentPlayer;
        }

        public void ChangeState()
        {
            bool isThereAWinner = board.CheckWinner() != PlayerType.None;

            if (++_moveCount >= 9 && !isThereAWinner)
            {
                UpdateStatus($"It's a Draw!");
                return;
            }
            
            if (isThereAWinner)
            {
                var winner = board.CheckWinner();
                UpdateStatus($"{winner} Wins!");
                
                return;
            }
            
            if (currentPlayer == PlayerType.X)
            {
                GameStateManager.SetState(new OTurnState(), this);
            }
            else if (currentPlayer == PlayerType.O)
            {
                GameStateManager.SetState(new XTurnState(), this);
            }
            
            UpdateStatus(currentPlayer == PlayerType.X? "Player X" : "Player O");
        }

        public void DisableBoard()
        {
            foreach (var cellView in cellViews)
            {
                cellView.SetInteraction(false);
            }
        }
    }
}
