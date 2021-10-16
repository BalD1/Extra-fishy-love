using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Characters
{
    [Space]
    [Header("Enemy Related")]
    private Transform player;

    private void Start()
    {
        player = GameManager.Instance.Player.transform;
        CallStart();
    }

    private void FixedUpdate()
    {
        TranslateTo(player);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.GetComponent<Player>().TakeDamages(characterStats.damages);
        }
    }

}
