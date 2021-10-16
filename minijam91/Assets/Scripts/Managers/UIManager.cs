using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region instance
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if(instance == null)
                Debug.LogError("UIManager instance not found.");

            return instance;
        }
    }
    #endregion

    [Header("Menus")]
    [SerializeField] private GameObject optionsMenu;
    public GameObject OptionsMenu
    {
        get => optionsMenu;
    }

    [Header("InGame")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameoverMenu;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Modifie l'UI suivant le nouveau GameState
    /// <para>
    /// Note : <paramref name="gameState"/> renvoie la nouvelle valeur, <see cref="GameManager.GameState"/> renvoie l'ancienne
    /// </para>
    /// </summary>
    /// <param name="gameState"> the new state of game</param>
    public void WindowManager(GameManager.GameStates gameState)
    {
        switch(gameState)
        {
            case GameManager.GameStates.MainMenu:

                break;

            case GameManager.GameStates.InGame:
                if(GameManager.Instance.GameState == GameManager.GameStates.Pause)
                    pauseMenu.SetActive(false);
                break;

            case GameManager.GameStates.Pause:
                pauseMenu.SetActive(true);
                break;

            case GameManager.GameStates.Win:

                break;

            case GameManager.GameStates.Gameover:
                gameoverMenu.SetActive(true);
                break;

            default:
                Debug.LogError(gameState + " state not found in switch statement.");
                break;
        }
    }

    public void OnButtonClick(string button)
    {
        switch(button)
        {
            case "play":
                GameManager.Instance.GameState = GameManager.GameStates.InGame;
                break;

            case "continue":
                GameManager.Instance.GameState = GameManager.GameStates.InGame;
                break;

            case "mainmenu":
                GameManager.Instance.GameState = GameManager.GameStates.MainMenu;
                break;

            case "retry":
                GameManager.Instance.ResetScene();
                break;

            case "quit":
                GameManager.Instance.QuitGame();
                break;

            default:
                Debug.LogError(button + " not found in switch statement.");
                break;
        }
    }

    public void SetHPBar(ref GameObject bar, float max)
    {
        Slider fill = bar.GetComponent<Slider>();
        fill.maxValue = max;
        fill.value = max;
    }
    public void ModifyHPBar(ref GameObject bar, float health)
    {
        Slider fill = bar.GetComponent<Slider>();
        fill.value = health;
    }
}
