using System;
using UnityEngine;
using Cell.Controllers;
using UnityEngine.UI;
using Managers;
using Model;

namespace Cell.View
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private Sprite _xSprite;
        [SerializeField] private Sprite _oSprite;
        

        public void UpdateCell(PlayerMove playerMove)
        {
            Debug.Log(playerMove);
            GetComponent<Image>().sprite = playerMove == PlayerMove.X ? _xSprite : _oSprite;
        }
    }
}

