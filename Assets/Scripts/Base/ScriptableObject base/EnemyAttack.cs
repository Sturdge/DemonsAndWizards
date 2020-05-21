using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttack", menuName = "Attacks/EnemyAttack")]
public class EnemyAttack : Attack
{
    private MobBase parent;

    public void Initialisation(MobBase controller)
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

            Debug.DrawLine(parent.transform.position, parent.transform.forward * 2);
            if (projectile != null)
            {
                parent.ObjectPooler.SpawnFromPool("Projectile", parent.transform);
            }
            else
            {
                if (Physics.Raycast(parent.transform.position, parent.transform.forward * parent.EntityData.AttackRange, out RaycastHit hit))
                {
                    Entity target = hit.collider.GetComponent<Entity>();
                    if (hit.collider.transform == parent.CurrentTarget)
                    {
                        if (target != null)
                            target.TakeDamage(BaseDamage);
                    }
                }
            }
            isOnCooldown = true;
        }
    }
}

