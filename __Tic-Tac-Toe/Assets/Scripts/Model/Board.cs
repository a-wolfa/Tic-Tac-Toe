using System.Linq;
using UnityEngine;

namespace Model
{
    public class Board
    {
        private readonly Slot[,] _slots = new Slot[3,3];

        public void UpdateBoard(int row, int column, Slot selectedSlot)
        {
            _slots[row, column] = selectedSlot;
        }

        public Slot[,] GetBoard()
        {
            return _slots;
        }
    }
}
