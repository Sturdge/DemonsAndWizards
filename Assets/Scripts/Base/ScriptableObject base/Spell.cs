using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewSpell", menuName = "Attacks/Spell")]
public class Spell : ScriptableObject
{
    [Header("Spell Attributes")]
    [SerializeField]
    private int _baseDamage;
    [SerializeField]
    private float cooldown;
    [SerializeField]
    private StatusEffect Auxiliary;

    private bool isOnCooldown;
    private float cooldownTimer;
    private PlayerController parent;
    [SerializeField]
    private GameObject particle;

    public int BaseDamage => _baseDamage;

    public void Initialisation(PlayerController controller)
    {
        isOnCooldown = false;
        cooldownTimer = 0;
        parent = controller;
        //particle = parent.transform.Find("DefaultAttack").GetComponent<ParticleSystem>();
        Debug.Log(particle);
    }

    public void DoAttack()
    {
        if (!isOnCooldown)
        {
            Instantiate(particle, parent.SpellPoint.position, parent.transform.rotation);
            Debug.Log("Shoot");
            isOnCooldown = true;
        }
    }

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