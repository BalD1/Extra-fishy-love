using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    [SerializeField] private float duration = 5f;

    void Start()
    {
        StartCoroutine(WaitForTheEnd(duration));
    }

    private IEnumerator WaitForTheEnd(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
