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

    [SerializeField] protected GameObject HUD;
    [SerializeField] protected GameObject healthBar;
    [SerializeField] protected float offsetHUD = 1.5f;

    [Header("Damages Flash")]
    [SerializeField] protected float flashTime = 0.125f;
    [SerializeField] protected Material flashMaterial;
    [SerializeField] protected Material originalMaterial;
    protected Coroutine flashCourtine;

    protected bool invincible;

    protected delegate void Death();
    protected Death _Death;

    protected void CallStart()
    {
        _Death += Die;
        characterStats = character.CharacterStats;
        if(healthBar != null)
            UIManager.Instance.SetHPBar(ref healthBar, characterStats.maxHP);
    }

    #region Movements

    protected void Translate(Vector2 direction)
    {
        body.velocity = new Vector2(characterStats.speed * direction.x, direction.y);
    }
    protected void TranslateTo(Transform target)
    {
        Vector2 direction = target.transform.position - this.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        body.rotation = angle;
        body.MovePosition((Vector2)this.transform.position + (direction * characterStats.speed * Time.deltaTime));
    }
    protected void TranslateHUD()
    {
        Vector2 pos = this.transform.position;
        pos.y = this.transform.position.y + offsetHUD;
        HUD.transform.position = pos;
    }

    #endregion

    #region Damages / Heal

    public void TakeDamages(int amount)
    {
        if(!invincible && amount != 0)
        {
            amount = Mathf.Abs(amount);
            ChangeHP(-amount);
            DamagesFeedback();
            if (characterStats.invincibleTime > 0)
            {
                invincible = true;
                StartCoroutine(Invincibility(characterStats.invincibleTime));
            }
        }
    }

    public void Heal(int amount)
    {
        amount = Mathf.Abs(amount);
        ChangeHP(amount);
    }

    private void ChangeHP(int amount)
    {
        characterStats.currentHP = Mathf.Clamp(characterStats.currentHP + amount, 0, characterStats.maxHP);
        FillHPBar();
        if(characterStats.currentHP <= 0)
            _Death();

    }

    protected void Die()
    {

    }

    protected void DamagesFeedback()
    {
        if(flashCourtine != null)
            StopCoroutine(flashCourtine);

        flashCourtine = StartCoroutine(Flash(flashTime));
    }

    protected void FillHPBar()
    {
        if(healthBar != null)
            UIManager.Instance.ModifyHPBar(ref healthBar, characterStats.currentHP);
    }

    private IEnumerator Flash(float time)
    {
        this.sprite.material = flashMaterial;
        yield return new WaitForSeconds(time);
        this.sprite.material = originalMaterial;
    }

    private IEnumerator Invincibility(float time)
    {
        yield return new WaitForSeconds(time);
        invincible = false;
    }

    #endregion

    #region prints
    public void PrintCharacter()
    {
        character.PrintCharacter(characterStats);
    }
    public void PrintCharacterHP()
    {
        character.PrintCharacterHP(characterStats);
    }
    #endregion
}
