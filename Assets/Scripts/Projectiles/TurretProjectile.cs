using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : Projectile
{
    private MobBase target;

    [SerializeField]
    private new int damage;

    public void SetTarget(MobBase t)
    {
        target = t;
        transform.LookAt(target.transform);
    }

    protected override void CheckCollision(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<MobBase>().TakeDamage(damage);
            
        }
    }
}
