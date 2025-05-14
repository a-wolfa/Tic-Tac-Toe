using Managers;
using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Button vsPlayer;
    [SerializeField] Button vsAI;

    private GameManager gameManager;
    private ViewManager viewManager;

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
        gameManager = FindFirstObjectByType<GameManager>();
        viewManager = FindFirstObjectByType<ViewManager>();
    }

    private void OnPlayerClicked(PlayerType playerType)
    {
        gameManager.playerOType = playerType;
        viewManager.UnLoadScene("Menu");
    }
}
