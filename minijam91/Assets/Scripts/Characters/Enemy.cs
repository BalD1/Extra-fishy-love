using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Characters
{
    [Space]
    [Header("Enemy Related")]
    [SerializeField] private GameObject root;

    private Transform player;

    private void Start()
    {
        player = GameManager.Instance.Player.transform;
        _Death += EnemyDeath;
        CallStart();
    }

    private void FixedUpdate()
    {
        TranslateTo(player);
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
    }

}
