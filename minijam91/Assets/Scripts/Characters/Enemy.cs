using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Characters
{
    [Space]
    [Header("Enemy Related")]
    [SerializeField] private GameObject root;
    [SerializeField] private int chancesToTargetFishtank = 5;
    private Transform target;

    private void Start()
    {
        int r = Random.Range(0, chancesToTargetFishtank + 1);

        if(Random.Range(0, chancesToTargetFishtank + 1) == 0)
            target = GameManager.Instance.Player.transform;
        else
            target = GameManager.Instance.FishTank.transform;
            
        _Death += EnemyDeath;
        CallStart();
    }

    private void FixedUpdate()
    {
        TranslateTo(target);
        TranslateHUD();
    }

    protected void EnemyDeath()
    {
        Destroy(root);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.GetComponent<Player>().TakeDamages(characterStats.damages);
        }
        if (collision.tag.Equals("Fishtank"))
        {
            collision.GetComponent<Fishtank>().TakeDamages(characterStats.damages);
        }
    }

}
