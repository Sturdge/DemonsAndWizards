using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Attributes")]
    [SerializeField]
    private string _name;
    [SerializeField]
    private string _description;
    [SerializeField]
    private object temp;
    [SerializeField]
    private StatusEffect statusEffect;
    [SerializeField, Range(0, 1)]
    private float statusChance;

    private float damage;

    public string Name => _name;
    public string Description => _description;

    public void SetDamage(int baseDamage, int skillLevel)
    {
        damage = baseDamage * (skillLevel * 0.5f);

    }

    private void OnParticleCollision(GameObject other)
    {
        Entity target = other.GetComponent<Entity>();
        if (target != null)
        {
            target.TakeDamage(damage);
            float statusRoll = Random.Range(0, 1);
            if (statusEffect != null)
            {
                if (statusRoll <= statusChance)
                {
                    Debug.Log("Inflicted");
                    target.SetStatus(statusEffect);
                }
            }
        }
    }
}