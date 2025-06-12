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

        [SerializeField] private Sprite _empty;
        
        public void UpdateCell(PMove playerMove)
        {
            var mark = GetComponent<Image>();

            mark.sprite = playerMove switch
            {
                PMove.X => _xSprite,
                PMove.O => _oSprite,
                _ => _empty
            };
        }
    }
}

