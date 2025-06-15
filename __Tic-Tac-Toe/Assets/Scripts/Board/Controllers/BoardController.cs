using System;
using Board.Model;
using Board.View;
using Cell.Controllers;
using Cell.Model;
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
            InitBoardModel();
        }

        public void InitBoardModel()
        {
            _cellControllers = FindObjectsByType<CellController>(FindObjectsSortMode.None);

            CellModel[,] cellModels = new CellModel[3, 3];

            foreach (var cellController in _cellControllers)
            {
                var row = cellController.GetModel().Row;
                var column = cellController.GetModel().Column;

                cellModels[row, column] = cellController.GetModel();
            }

            _boardModel = new BoardModel(cellModels);
        }

        public void Reset()
        {
            _boardModel.ResetBoard();
        }

        public BoardModel GetModel()
        {
            return _boardModel;
        }
    }
}