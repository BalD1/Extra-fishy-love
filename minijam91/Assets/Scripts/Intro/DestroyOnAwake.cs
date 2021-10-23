using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAwake : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private GameObject blackBars;
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject dialogueBoxes;
    private void Start()
    {
        Destroy(dialogueBoxes);
        Destroy(blackBars);
        mainCam.transform.position = new Vector3(0, 0, -10);
        GameManager.Instance.GameState = GameManager.GameStates.InGame;
        Destroy(root);
    }
}
