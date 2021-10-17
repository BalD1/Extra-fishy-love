using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] private WeaponScriptable weapon;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private ParticleSystem burst;
    [SerializeField] private ParticleSystem bubbles;
    private WeaponScriptable.stats weaponStats;

    private bool canFire;

    private void Start()
    {
        weaponStats = weapon.WeaponStats;
        canFire = true;
    }

    private void Update()
    {
        if(GameManager.Instance.GameState == GameManager.GameStates.InGame)
            Fire();
    }

    public void Fire()
    {
        if(canFire)
        {
            if(Input.GetMouseButton(0))
            {
                burst.Play();
                bubbles.Play();
                canFire = false;
                Projectile firedProjectile = PoolManager.Instance.SpawnFromPool(PoolManager.tags.Laser, firePoint.transform.position, this.transform.rotation).GetComponent<Projectile>();
                firedProjectile.damagesToInflict = weaponStats.damages;

                StartCoroutine(fireTimer(weaponStats.fireRate));
            }
        }
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
