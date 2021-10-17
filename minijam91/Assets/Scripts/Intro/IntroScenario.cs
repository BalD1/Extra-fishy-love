using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScenario : MonoBehaviour
{
    [SerializeField] private GameObject DialogueHolder1;
    [SerializeField] private GameObject DialogueHolder2;
    [SerializeField] private GameObject Player;
    [SerializeField] private Animator FishAnimator;

    private void Awake()
    {
       StartCoroutine(introSequence());
    }

    private IEnumerator introSequence()
    {
        yield return new WaitUntil(() => !DialogueHolder1.activeInHierarchy);
        FishAnimator.Play("SwimToUFO");
        yield return new WaitForSeconds(4f);
        Player.transform.Rotate(-180.0f, 0.0f, -180.0f, Space.Self);
        DialogueHolder2.SetActive(true);
        yield return new WaitUntil(() => !DialogueHolder2.activeInHierarchy);
        Debug.Log("finish");
        SceneManager.LoadScene("MainScene");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
