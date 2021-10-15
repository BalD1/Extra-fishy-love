using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region instance
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
                Debug.LogError("GameManager instance not found.");

            return instance;
        }
    }
    #endregion

    public string SceneName { get => SceneManager.GetActiveScene().name; }

    #region GameStates

    public enum GameStates
    {
        MainMenu,
        InGame,
        Pause,
        Win,
        Gameover,
    }
    public GameStates gameState;

    /// <summary>
    /// Change le <see cref="GameManager.GameState"/> par la valeur donnée, ou retourne le <see cref="GameManager.GameState"/> actuel
    /// <para>
    /// Note : la valeur de <see cref="GameManager.gameState"/> n'est modifiée qu'à la fin de l'appel des fonctions,
    /// donc <see cref="GameManager.gameState"/> retournera l'ancienne valeur, et <value> value </value> retournera l'actuelle
    /// </para>
    /// </summary>
    public GameStates GameState
    {
        set
        {
            switch(value)
            {
                case GameStates.MainMenu:
                    if(gameState != GameStates.MainMenu && SceneName.Equals("MainScene"))
                        SceneManager.LoadScene("MainMenu");

                    Time.timeScale = 0;
                    break;

                case GameStates.InGame:
                    if (gameState == GameStates.MainMenu && SceneName.Equals("MainMenu"))
                        SceneManager.LoadScene("MainScene");

                    if (gameState == GameStates.Pause)
                    {
                    }
                        Time.timeScale = 1;
                    break;

                case GameStates.Pause:

                    Time.timeScale = 0;
                    break;

                case GameStates.Win:

                    Time.timeScale = 0;
                    break;

                case GameStates.Gameover:

                    Time.timeScale = 0;
                    break;

                default:
                    Debug.LogError(value + " state not found in switch statement.");
                    break;
            }

            UIManager.Instance.WindowManager(value);

            gameState = value;
            
        }
        get => gameState;
    }

    #endregion

    private void Awake()
    {
        instance = this;
        if(SceneName.Equals("MainMenu"))
            GameState = GameStates.MainMenu;
        else
            GameState = GameStates.InGame;
    }

    public void QuitGame()
    {
        //
        Application.Quit();
    }

}
