using Model;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text statusText;
        
        public UnityEvent onMoved;
        public Slot selectedSlot;
        public Turn CurrentTurn { get; private set; }
        private Board _board;
        private Slot[,] _slots;
        
        private const int BoardSize = 3;
        private int _moveCount = 0;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            CurrentTurn = Turn.X;
            _board = new Board();
            _slots = _board.GetBoard();
            InitCommands();
        }

        private void InitCommands()
        {
            onMoved.AddListener(UpdateBoard);
            onMoved.AddListener(MakeTurn);
        }

        private void UpdateBoard()
        {
            var row = selectedSlot.row;
            var column = selectedSlot.column;
           
            _board.UpdateBoard(row, column, selectedSlot);
            _moveCount++;
            if (CheckForWinner())
            {
                statusText.text = $"{CurrentTurn} Won!";
                GameOver();
            }
            else if (_moveCount == 9)
            {
                statusText.text = "Draw!";
            }

        }

        private void MakeTurn()
        {
            CurrentTurn = (Turn) ((int)CurrentTurn * -1);
        }

        private void GameOver()
        {
            CurrentTurn = Turn.GameOver;
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
