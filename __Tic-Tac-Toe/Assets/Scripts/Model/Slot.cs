using Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Model
{
    public class Slot : MonoBehaviour
    {
        public int row;
        public int column;

        public Turn playedTurn;
    }
}