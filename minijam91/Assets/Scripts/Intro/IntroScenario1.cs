using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScenario1 : MonoBehaviour
{
    [SerializeField] private GameObject DialogueHolder1;
    [SerializeField] private GameObject DialogueHolder2;
    [SerializeField] private Animator UFOAnimator;
    [SerializeField] private GameObject Beam;
    [SerializeField] private GameObject Cow;
    [SerializeField] private GameObject Cam1;
    [SerializeField] private Animator BlackFadeOut;
    //[SerializeField] private GameObject Cam2;

    private float xPos;
    private bool finished = false;

    private void Awake()
    {
        StartCoroutine(introSequence());
    }

    private IEnumerator introSequence()
    {
        UFOAnimator.Play("Capture");
        yield return new WaitForSeconds(1f);

        //AudioManager.Instance.Play2DSound("Beam");
        Beam.SetActive(true);
        yield return new WaitForSeconds(1.8f);

        Destroy(Cow);
        Beam.SetActive(false);
        yield return new WaitForSeconds(1f);

        DialogueHolder1.SetActive(true);
        yield return new WaitUntil(() => !DialogueHolder1.activeInHierarchy);

        //AudioManager.Instance.Play2DSound("UFOEngine");
        UFOAnimator.Play("Scrolling");
        yield return new WaitForSeconds(4.5f);

        DialogueHolder2.SetActive(true);
        UFOAnimator.Play("PanikAnim");
        yield return new WaitUntil(() => !DialogueHolder2.activeInHierarchy);

        UFOAnimator.Play("Falling");
        //AudioManager.Instance.Play2DSound("Crash");
        BlackFadeOut.Play("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Intro2");
    }

    private void Update()
    {
        xPos = this.transform.position.x;
        Cam1.transform.position = new Vector3(xPos, 0f, -25f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
