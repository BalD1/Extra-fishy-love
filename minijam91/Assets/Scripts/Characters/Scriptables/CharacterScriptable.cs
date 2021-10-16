using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object", menuName = "ScriptableObject/ Character Configuration")]
public class CharacterScriptable : ScriptableObject
{
    [System.Serializable]
    public struct stats
    {
        public string name;
        public int maxHP;
        public int currentHP;
        public float speed;
        public float jumpSpeed;
        public int damages;
        public float invincibleTime;
    }
    public stats CharacterStats;

    #region prints
    public void PrintCharacter()
    {
        Debug.Log(CharacterStats.name + " : \n" +
                  "HP : " + CharacterStats.currentHP + " / " + CharacterStats.maxHP + "                " +
                  "Speed : " + CharacterStats.speed + "                " +
                  "Damages : " + CharacterStats.damages + "                " +
                  "Invincibility time : " + CharacterStats.invincibleTime);
    }
    public void PrintCharacter(CharacterScriptable.stats targetStats)
    {
        Debug.Log(targetStats.name + " : \n" +
                  "HP : " + targetStats.currentHP + " / " + targetStats.maxHP + "                " +
                  "Speed : " + targetStats.speed + "                " +
                  "Damages : " + targetStats.damages + "                " +
                  "Invincibility time : " + targetStats.invincibleTime);
    }
    public void PrintCharacterHP()
    {
        Debug.Log(CharacterStats.name + " : " + CharacterStats.currentHP + " / " + CharacterStats.maxHP);
    }
    public void PrintCharacterHP(CharacterScriptable.stats targetStats)
    {
        Debug.Log(targetStats.name + " : " + targetStats.currentHP + " / " + targetStats.maxHP);
    }
    #endregion
}
