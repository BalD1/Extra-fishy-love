using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] protected WeaponScriptable weapon;
    [SerializeField] protected GameObject firePoint;
    [SerializeField] protected ParticleSystem burst;
    [SerializeField] protected ParticleSystem bubbles;
    [SerializeField] protected AudioSource piou;
    protected WeaponScriptable.stats weaponStats;

    protected bool canFire;

    protected void CallStart()
    {
        weaponStats = weapon.WeaponStats;
        canFire = true;
    }
    protected void CallOnEnable()
    {
        StopAllCoroutines();
        canFire = true;
    }

    protected void OnFireEvents()
    {
        burst.Play();
        bubbles.Play();
        piou.Play();
        canFire = false;

        StartCoroutine(fireTimer(weaponStats.fireRate));
    }

    private IEnumerator fireTimer(float time)
    {
        yield return new WaitForSeconds(time);
        canFire = true;
    }

    #region prints
    protected void PrintWeapon()
    {
        weapon.PrintWeapon(weaponStats);
    }

    #endregion
}
