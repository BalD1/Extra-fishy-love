using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishtank : Characters
{
    [Header("Fishtank Related")]
    [SerializeField] private float minTimeBeforeAsk;
    [SerializeField] private float maxTimeBeforeAsk;

    private float timeBeforeFishDies;
    private float timerBeforeFishDies;

    [SerializeField] private GameObject bubble;
    [SerializeField] private Animator fishAnimator;

    private bool isAsking;

    private void Start()
    {
        timeBeforeFishDies = GameManager.Instance.GetAnimationLength(fishAnimator, "BubbleAnim");
        timerBeforeFishDies = timeBeforeFishDies;
        CallStart();
        StartCoroutine(AskTimer(maxTimeBeforeAsk));
        _Death += FishtankDeath;
    }

    private void Update()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.InGame && isAsking)
        {
            timerBeforeFishDies = timerBeforeFishDies - Time.deltaTime;

            if(timerBeforeFishDies <= 0)
                GameManager.Instance.GameState = GameManager.GameStates.Gameover;
        }
    }

    private void AskForSeaweed()
    {
        bubble.SetActive(true);
        isAsking = true;
        timerBeforeFishDies = timeBeforeFishDies;
    }

    private IEnumerator AskTimer(float time)
    {
        yield return new WaitForSeconds(time);
        AskForSeaweed();
    }

    private void FishtankDeath()
    {
        GameManager.Instance.GameState = GameManager.GameStates.Gameover;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && isAsking)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(player.HasSeaWeed)
            {
                player.HasSeaWeed = false;

                bubble.SetActive(false);    
                isAsking = false;
                timerBeforeFishDies = timeBeforeFishDies;

                float timeBeforeAsk = Random.Range(minTimeBeforeAsk, maxTimeBeforeAsk);
                StartCoroutine(AskTimer(timeBeforeAsk));
            }
        }
    }

}
