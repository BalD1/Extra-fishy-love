using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

                    break;

                case GameStates.InGame:

                    break;

                case GameStates.Pause:

                    break;

                case GameStates.Win:

                    break;

                case GameStates.Gameover:

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
    }
}
