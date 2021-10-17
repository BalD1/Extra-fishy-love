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

    [SerializeField] private GameObject player;
    public GameObject Player { get => player; }
    [SerializeField] private GameObject fishTank;
    public GameObject FishTank { get => fishTank; }
    public string SceneName { get => SceneManager.GetActiveScene().name; }

    #region GameStates

    public enum GameStates
    {
        MainMenu,
        intro,
        InGame,
        Pause,
        Win,
        Cinematic,
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
                    if(gameState != GameStates.MainMenu && !SceneName.Equals("MainMenu"))
                        SceneManager.LoadScene("Intro1");

                    Time.timeScale = 1;
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
                case GameStates.Cinematic:
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
    public float GetAnimationLength(Animator animator, string searchedAnimation)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            if(clip.name.Equals(searchedAnimation))
                return clip.length;
        }
        Debug.LogError(searchedAnimation + " not found in " + animator + ".");
        return 0;
    }

    public float GetPercentage(float value, float maxOfValue)
    {
        return (value / maxOfValue) * 100;
    }
    public float GetPercentage(int value, int maxOfValue)
    {
        return GetPercentage((float)value, (float)maxOfValue);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        //
        Application.Quit();
    }

}
