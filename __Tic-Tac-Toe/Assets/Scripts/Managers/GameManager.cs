using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public UnityEvent onGameOver;
        public UnityEvent onMoved;
        
        public enum Turn
        {
            X = -1,
            GameOver = 0,
            O = -1
        }

        public Turn CurrentTurn { get; private set; }

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            CurrentTurn = Turn.X;
            InitCommands();
        }

        private void InitCommands()
        {
            onMoved.AddListener(MakeTurns);
            onGameOver.AddListener(GameOver);
        }


        private void MakeTurns()
        {
            CurrentTurn = (Turn) ((int)CurrentTurn * -1);
        }

        private void GameOver()
        {
            CurrentTurn = Turn.GameOver;
        }
    }
}
