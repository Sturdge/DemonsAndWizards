using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpell", menuName = "Attacks/Spell")]
public class Spell : Attack
{
    private PlayerController parent;

    public void Initialisation(PlayerController controller)
    {
        isOnCooldown = false;
        cooldownTimer = 0;
        parent = controller;
        ray = new Ray(parent.transform.position, parent.transform.forward * 1);
    }

    public override void DoAttack()
    {
        if (!isOnCooldown)
        {
            if (projectile != null)
            {

                PlayerProjectile projectile = parent.ObjectPooler.SpawnFromPool("Projectile", parent.SpellPoint).GetComponent<PlayerProjectile>();
                projectile.SetDamage(BaseDamage, parent.SpellLevel);
                isOnCooldown = true;
            }
        }
    }

    
}