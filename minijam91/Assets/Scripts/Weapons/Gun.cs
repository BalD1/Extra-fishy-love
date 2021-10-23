using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapons
{
    private Projectile firedProjectile;

    private void Start()
    {
        CallStart();
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
                OnFireEvents();
                firedProjectile = PoolManager.Instance.SpawnFromPool(PoolManager.tags.Laser, firePoint.transform.position, this.transform.rotation).GetComponent<Projectile>();
                firedProjectile.damagesToInflict = weaponStats.damages;
            }
        }
    }
    private void OnEnable()
    {
        CallOnEnable();
    }
}
