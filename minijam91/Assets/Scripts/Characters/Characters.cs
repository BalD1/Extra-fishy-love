using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    [Header("Character Components")]
    [SerializeField] protected CharacterScriptable character;
    protected CharacterScriptable.stats characterStats;

    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Rigidbody2D body;

    protected void CallStart()
    {
        characterStats = character.CharacterStats;
    }

    protected void Translate(Vector2 direction)
    {
        body.velocity = new Vector2(characterStats.speed * direction.x, direction.y);
    }
    protected void TranslateTo(Transform target)
    {
        this.transform.Translate(target.position);
    }

    public void TakeDamages(int amount)
    {
        ChangeHP(-amount);
    }

    public void Heal(int amount)
    {
        ChangeHP(amount);
    }

    private void ChangeHP(int amount)
    {
        characterStats.currentHP = Mathf.Clamp(characterStats.currentHP + amount, 0, characterStats.maxHP);
        if(characterStats.currentHP <= 0)
            Death();
    }

    protected void Death()
    {
        Destroy(this.gameObject);
    }

    #region prints
    protected void PrintCharacter()
    {
        character.PrintCharacter();
    }
    protected void PrintCharacter(Characters target)
    {
        target.PrintCharacter();
    }
    #endregion
}
