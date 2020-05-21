using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    public void SetDamage(int baseDamage, int skillLevel)
    {
        damage = baseDamage * skillLevel;
    }

    protected override void CheckCollision(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            CollisionLogic(other);
        }
        else
        {
            if(!persists)
                gameObject.SetActive(false);
        }
    }

}