using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    [SerializeField] protected CharacterScriptable character;

    protected void CallStart()
    {

    }

    protected void TakeDamages(int amount)
    {
        ChangeHP(-amount);
    }

    protected void Heal(int amount)
    {
        ChangeHP(amount);
    }

    private void ChangeHP(int amount)
    {

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
