using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScenario1 : MonoBehaviour
{
    [SerializeField] private GameObject DialogueHolder1;
    [SerializeField] private GameObject DialogueHolder2;
    [SerializeField] private Animator UFOAnimator;
    [SerializeField] private GameObject Beam;
    [SerializeField] private GameObject Cow;
    [SerializeField] private GameObject Cam1;
    //[SerializeField] private GameObject Cam2;

    private float xPos;

    private void Awake()
    {
        StartCoroutine(introSequence());
    }

    private IEnumerator introSequence()
    {
        UFOAnimator.Play("Capture");
        yield return new WaitForSeconds(1f);
        Beam.SetActive(true);
        yield return new WaitForSeconds(1.8f);
        Destroy(Cow);
        Beam.SetActive(false);
        yield return new WaitForSeconds(1f);
        DialogueHolder1.SetActive(true);
        yield return new WaitUntil(() => !DialogueHolder1.activeInHierarchy);
        Cam1.SetActive(true);
        //Cam2.SetActive(false);
        UFOAnimator.Play("Scrolling");
        yield return new WaitForSeconds(5f);
        DialogueHolder2.SetActive(true);
        //Cam1.transform.position = Cam2.transform.position;
        Cam1.SetActive(false);
        //Cam2.SetActive(true);

        //DialogueHolder1.SetActive(true);




        //SceneManager.LoadScene("Intro2");
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
