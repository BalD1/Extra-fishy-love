using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapons
{
    [SerializeField] private int projectilesToFire = 5;
    [SerializeField] private float[] spreads;

    private void Start()
    {
        CallStart();
    }

    private void Update()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.InGame)
            Fire();
    }

    public void Fire()
    {
        if (canFire)
        {
            if (Input.GetMouseButton(0))
            {
                OnFireEvents();

                for (int i = 0; i < projectilesToFire; i++)
                {
                    Projectile currentProjectile;
                    currentProjectile = PoolManager.Instance.SpawnFromPool(PoolManager.tags.Laser, firePoint.transform.position, this.transform.rotation).GetComponent<Projectile>();

                    float rotation = spreads[i];
                    currentProjectile.transform.Rotate(new Vector3(0, 0, rotation));

                    currentProjectile.damagesToInflict = weaponStats.damages;
                }

            }
        }
    }
    private void OnEnable()
    {
        CallOnEnable();
    }
}
