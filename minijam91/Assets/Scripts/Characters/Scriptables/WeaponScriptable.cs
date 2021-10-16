using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object", menuName = "ScriptableObject/ Weapon Configuration")]
public class WeaponScriptable : ScriptableObject
{
    [System.Serializable]
    public struct stats
    {
        public string name;
        public float fireRate;
        public int maxAmmo;
        public int currentAmmo;
        public int damages;
    }
    public stats WeaponStats;

    #region prints
    public void PrintWeapon()
    {
        Debug.Log(WeaponStats.name + " : \n" +
                  "Ammo : " + WeaponStats.currentAmmo + " / " + WeaponStats.maxAmmo + "                " +
                  "FireRate : " + WeaponStats.fireRate + "                " +
                  "Damages : " + WeaponStats.damages);
    }

    public void PrintWeapon(WeaponScriptable.stats targetStats)
    {
        Debug.Log(targetStats.name + " : \n" +
             "Ammo : " + targetStats.currentAmmo + " / " + targetStats.maxAmmo + "                " +
             "FireRate : " + targetStats.fireRate + "                " +
             "Damages : " + targetStats.damages);
    }
    #endregion
}
