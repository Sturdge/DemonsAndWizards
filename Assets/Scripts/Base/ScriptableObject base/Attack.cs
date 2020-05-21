using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : ScriptableObject
{
    [Header("Spell Attributes")]
    [SerializeField]
    private int _baseDamage = 0;
    [SerializeField]
    private float cooldown = 0;
    [SerializeField]
    protected UnityEngine.GameObject projectile = null;

    protected bool isOnCooldown;
    protected float cooldownTimer;
    protected Ray ray;

    public int BaseDamage => _baseDamage;

    public abstract void DoAttack();

    public void UpdateCooldown(float deltatime)
    {
        if (isOnCooldown)
        {
            if (cooldownTimer < cooldown)
            {
                cooldownTimer += deltatime;
                CheckTimerEnd();
            }
        }
    }

    private void CheckTimerEnd()
    {
        if (cooldownTimer >= cooldown)
        {
            cooldownTimer = 0;
            isOnCooldown = false;
        }
    }
}
