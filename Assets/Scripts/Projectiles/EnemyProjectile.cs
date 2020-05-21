using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    public void SetDamage(int baseDamage)
    {
        damage = baseDamage;
    }

    protected override void CheckCollision(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollisionLogic(other);
        }
        else if(other.CompareTag("Nexus"))
        {
            CollisionLogic(other);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
