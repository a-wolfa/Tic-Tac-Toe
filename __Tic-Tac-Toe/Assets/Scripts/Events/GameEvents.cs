using Model;
using UnityEngine.Events;

namespace Events
{
    public static class GameEvents
    {
        public static UnityEvent<GameResult> GameOver;
        public static UnityEvent CellUpdated = new UnityEvent();
    }
}