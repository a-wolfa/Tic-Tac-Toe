using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] Button vsPlayer;
        [SerializeField] Button vsAI;

        private GameManager _gameManager;
        private ViewManager _viewManager;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            InitCommands();
        }

        private void InitCommands()
        {
            vsPlayer.onClick.AddListener(() => OnPlayerClicked(PlayerType.Human));
            vsAI.onClick.AddListener(() => OnPlayerClicked(PlayerType.AI));
        }

        private void Start()
        {
            _gameManager = FindFirstObjectByType<GameManager>();
            _viewManager = FindFirstObjectByType<ViewManager>();
        }

        private void OnPlayerClicked(PlayerType playerType)
        {
            _gameManager.playerOType = playerType;
            _viewManager.UnLoadScene("Menu");
        }
    }
}
