using Core;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View
{
    public class CellView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Sprite xSprite;
        [SerializeField] private Sprite oSprite;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Cell cell;
        
        private Image _spriteRenderer;
        private bool _interactable = true;

        private void Awake()
        {
            Init();
        }

        private void InitComponents()
        {
            _spriteRenderer = GetComponent<Image>();
        }

        private void Init()
        {
            InitComponents();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_interactable) return;
            
            _interactable = false;
            
            var currentPlayer = gameManager.GetActivePlayer();
            Debug.Log(currentPlayer);
            _spriteRenderer.sprite = currentPlayer switch
            {
                PlayerType.X => xSprite,
                PlayerType.O => oSprite,
                _ => _spriteRenderer.sprite
            };
            cell.SetCell(currentPlayer);
            gameManager.ChangeState();
        }

        public void SetInteraction(bool enable)
        {
            _interactable = enable;
        }
        
        public Cell Cell
        {
            get { return cell; }
        }
    }
}