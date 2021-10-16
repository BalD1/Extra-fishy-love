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
    }
    public stats CharacterStats;

    #region prints
    public void PrintCharacter()
    {
        Debug.Log(CharacterStats.name + " : \n" +
                  "HP : " + CharacterStats.currentHP + " / " + CharacterStats.maxHP + "                " +
                  "Speed : " + CharacterStats.speed + "                " +
                  "Damages : " + CharacterStats.damages);
    }
    public void PrintCharacterHP()
    {
        Debug.Log(CharacterStats.name + " : " + CharacterStats.currentHP + " / " + CharacterStats.maxHP);
    }
    #endregion
}
