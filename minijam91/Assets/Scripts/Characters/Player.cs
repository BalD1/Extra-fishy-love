using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Characters
{

    private void Start()
    {
        PrintCharacter();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void Pause()
    {
        if(GameManager.Instance.GameState == GameManager.GameStates.InGame)
            GameManager.Instance.GameState = GameManager.GameStates.Pause;
        else if(GameManager.Instance.GameState == GameManager.GameStates.Pause)
            GameManager.Instance.GameState = GameManager.GameStates.InGame;
    }
}
