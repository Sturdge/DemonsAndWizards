using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    private EntityData _entityData;

    [Header("Graphical Properties")]
    [SerializeField]
    private StatusText statusText;
    [SerializeField]
    private DamageText[] damageText;

    private float currentHitPoints;
    public float MovementMultiplier { get; protected set; }
    public List<StatusEffect> StatusEffects { get; private set; }

    public EntityData EntityData => _entityData;
    private int currentDamageText;
    protected void Initialisation()
    {
        currentHitPoints = _entityData.MaxHitPoints;
        StatusEffects = new List<StatusEffect>();
        currentDamageText = 0;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (StatusEffects != null)
        {
            for (int i = 0; i < StatusEffects.Count; i++)
            {
                StatusEffects[i].DoStatusEffect(Time.deltaTime);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHitPoints -= damage;
        DisplayDamageText(damage);
        //if (currentHitPoints <= 0)
            //GameManager.Instance.OnEntityDie(this);
    }

    private void DisplayDamageText(float damage)
    {
        damageText[currentDamageText].Activate(damage);
        currentDamageText++;
        if (currentDamageText == damageText.Length)
            currentDamageText = 0;
    }

    public void SetStatus(StatusEffect newStatus)
    {
        if (!StatusEffects.Contains(newStatus))
        {
            statusText.Activate(newStatus);
            StatusEffects.Add(newStatus);
            StatusEffects[StatusEffects.Count - 1].OnStart(this);
        }
    }

    public void SetMovementMultiplier(float newMultiplier)
    {
        MovementMultiplier = newMultiplier;
    }
}
