using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject introEnder;
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            introEnder.SetActive(true);

    }
}
