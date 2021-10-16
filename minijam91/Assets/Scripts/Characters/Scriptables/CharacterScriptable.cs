using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObject/ Character Configuration")]
public class CharacterScriptable : ScriptableObject
{
    [System.Serializable]
    public struct stats
    {
        public string name;
        public int maxHP;
        public int currentHP;
        public int speed;
        public int damages;
    }
    public stats CharacterStats;

    #region prints
    public void PrintCharacter()
    {
        Debug.Log(name + " : \n" +
                  "HP : " + CharacterStats.currentHP + " / " + CharacterStats.maxHP + "                " +
                  "Speed : " + CharacterStats.speed + "                " +
                  "Damages : " + CharacterStats.damages);
    }
    public void PrintCharacterHP()
    {
        Debug.Log(name + " : " + CharacterStats.currentHP + " / " + CharacterStats.maxHP);
    }
    #endregion
}
