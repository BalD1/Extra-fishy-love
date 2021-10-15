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

                break;

            case GameManager.GameStates.Pause:

                break;

            case GameManager.GameStates.Win:

                break;

            case GameManager.GameStates.Gameover:

                break;
            default:
                Debug.LogError(gameState + " state not found in switch statement.");
                break;
        }
    }
}
