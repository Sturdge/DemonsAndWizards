using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTurret : Turret
{
    [SerializeField]
    private int bursts;

    private bool canFire;
    private ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = GetComponent<ObjectPooler>();
        canFire = true;
    }

    protected override void TurretBehaviour()
    {
        if(canFire)
        {
            for(int i = 0; i < bursts; i++)
            {
                int target = Random.Range(0, targets.Count);
                TurretProjectile projectile = objectPooler.SpawnFromPool("Projectile", gameObject.transform).GetComponent<TurretProjectile>();
                projectile.SetTarget(targets[target]);
            }

            StartCoroutine(FireCooldown());
        }
    }

    private IEnumerator FireCooldown()
    {
        canFire = false;

        yield return new WaitForSeconds(Data.Cooldown);

        canFire = true;

    }

}
