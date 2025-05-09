using Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Model
{
    public class Cell : MonoBehaviour
    {
        public int row;
        public int column;

        [FormerlySerializedAs("playedTurn")] public PlayerMove playedTurn;
    }
}