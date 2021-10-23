using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataKeep : MonoBehaviour
{
    public static bool playIntro = true;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
