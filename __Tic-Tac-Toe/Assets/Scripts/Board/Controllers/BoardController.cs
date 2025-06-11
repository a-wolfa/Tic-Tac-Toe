using System;
using Board.Model;
using Board.View;
using Cell.Controllers;
using Model;
using UnityEngine;

namespace Board.Controllers
{
    public class BoardController : MonoBehaviour
    {
        private BoardModel _boardModel;
        private BoardView _boardView;
        
        private CellController[] _cellControllers;


        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _boardModel = new BoardModel();
        }
        
        public void PlaceMark(int row, int column, PlayerMove mark)
        {
            // TODO
        }
    }
}