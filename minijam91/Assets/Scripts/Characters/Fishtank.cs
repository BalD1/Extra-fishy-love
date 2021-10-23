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
    [SerializeField] private SpriteRenderer breakRenderer;
    [SerializeField] private Animator fishAnimator;
    [SerializeField] private Animator breakAnimator;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private Sprite explodedSprite;
    [SerializeField] private GameObject breakObject;

    private bool isAsking;
    private Coroutine breakFlashCoroutine;

    private void Start()
    {
        timeBeforeFishDies = GameManager.Instance.GetAnimationLength(fishAnimator, "BubbleAnim");
        timerBeforeFishDies = timeBeforeFishDies;
        CallStart();

        GameManager.Instance._GameStart += OnGameStart;

        _Death += FishtankDeath;
        _TookDamages += BreakAnimation;
    }

    private void OnGameStart()
    {
        StopAllCoroutines();
        bubble.SetActive(false);
        isAsking = false;
        timerBeforeFishDies = timeBeforeFishDies;
        this.Heal(characterStats.maxHP);
        StartCoroutine(AskTimer(maxTimeBeforeAsk));
    }

    private void Update()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.InGame && isAsking)
        {
            timerBeforeFishDies = timerBeforeFishDies - Time.deltaTime;

            if(timerBeforeFishDies <= 0)
            {
                UIManager.Instance.gameOverText = "GAME OVER \n YOU DIDN'T FED THE FISH";
                GameManager.Instance.GameState = GameManager.GameStates.Gameover;
            }
        }
    }

    private void AskForSeaweed()
    {
        bubble.SetActive(true);
        isAsking = true;
        timerBeforeFishDies = timeBeforeFishDies;
    }

    private void FishtankDeath()
    {
        StopCoroutine(breakFlashCoroutine);
        this.sprite.material = originalMaterial;
        breakObject.SetActive(false);
        this.animator.enabled = false;
        this.sprite.sprite = explodedSprite;
        explosion.Play();
        GameManager.Instance.GameState = GameManager.GameStates.Cinematic;
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.Play2DSound(AudioManager.ClipsTags.glassExplosion);
        UIManager.Instance.gameOverText = "GAME OVER \n THE SHIP BROKE";
        StartCoroutine(WaitBeforeGameover(3));
    }

    private IEnumerator WaitBeforeGameover(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        GameManager.Instance.GameState = GameManager.GameStates.Gameover;
    }

    private void BreakAnimation()
    {
        AudioManager.Instance.Play2DSound(AudioManager.ClipsTags.glasshit);
        float healthPercentage = GameManager.Instance.GetPercentage(characterStats.currentHP, characterStats.maxHP);
        BreakDamagesFeedback();

        if(healthPercentage >= 75)
            breakAnimator.SetTrigger("firstquarter");
        else if(healthPercentage < 75 && healthPercentage >= 50)
            breakAnimator.SetTrigger("secondquarter");
        else if(healthPercentage < 50 && healthPercentage >= 25)
            breakAnimator.SetTrigger("thirdquarter");
        else if(healthPercentage < 25)
            breakAnimator.SetTrigger("lastquarter");
    }

    private void BreakDamagesFeedback()
    {
        if(breakFlashCoroutine != null)
            StopCoroutine(breakFlashCoroutine);

        breakFlashCoroutine = StartCoroutine(Flash(flashTime));
    }

    private IEnumerator Flash(float time)
    {
        breakRenderer.material = flashMaterial;
        yield return new WaitForSeconds(time);
        breakRenderer.material = originalMaterial;
    }


    private IEnumerator AskTimer(float time)
    {
        yield return new WaitForSeconds(time);
        AskForSeaweed();
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
                this.Heal(characterStats.maxHP / 4);

                float timeBeforeAsk = Random.Range(minTimeBeforeAsk, maxTimeBeforeAsk);
                StartCoroutine(AskTimer(timeBeforeAsk));
            }
        }
    }

}
