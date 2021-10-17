using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Characters
{
    [Space]
    [Header("Enemy Related")]
    [SerializeField] private GameObject root;
    [SerializeField] private int chancesToTargetFishtank = 5;
    [SerializeField] private float minDistanceBeforeAttack;
    [SerializeField] private bool showDistance = true;
    [SerializeField] private GameObject bloodOnDeath;
    private Transform target;

    [SerializeField] [HideInInspector] private bool playSound;

    private enum EnemyState
    {
        Moving, 
        Attacking,
    }
    private EnemyState state;

    private void Start()
    {
        state = EnemyState.Moving;
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
        CheckDistance();
        MoveToTarget(target);
        TranslateHUD();
        if(state.Equals(EnemyState.Attacking))
            Attack();
    }

    private void EnemyDeath()
    {
        Instantiate(bloodOnDeath, this.transform.position, Quaternion.identity);
        AudioManager.Instance.Play2DSound(AudioManager.ClipsTags.E_01_die);
        Destroy(root);
    }

    private void MoveToTarget(Transform target)
    {
        Vector2 direction = target.transform.position - this.transform.position;
        float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        body.rotation = angle;

        if (state == EnemyState.Moving)
            body.MovePosition((Vector2)this.transform.position + (direction * characterStats.speed * Time.deltaTime));

    }

    private void Attack()
    {
        body.velocity = Vector2.zero;
        if(Vector2.Distance(this.transform.position, target.position) > minDistanceBeforeAttack)
        {
            state = EnemyState.Moving;
        }
        animator.SetTrigger("attack");

        if(playSound)
            PlayAttackSound();
    }

    private void PlayAttackSound()
    {
        AudioManager.Instance.Play2DSound(AudioManager.ClipsTags.E01_attack);
    }

    private void CheckDistance()
    {
        if(Vector2.Distance(this.transform.position, target.position) <= minDistanceBeforeAttack)
            state = EnemyState.Attacking;
        else
            state = EnemyState.Moving;
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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(showDistance)
            Gizmos.DrawWireSphere(this.transform.position, minDistanceBeforeAttack);
    }
#endif

}
