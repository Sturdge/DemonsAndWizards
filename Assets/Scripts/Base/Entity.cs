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

    private float currentHitPoints;
    public float MovementMultiplier { get; protected set; }
    public List<StatusEffect> StatusEffects { get; private set; }

    public EntityData EntityData => _entityData;

    private void Awake()
    {
        currentHitPoints = _entityData.MaxHitPoints;
        StatusEffects = new List<StatusEffect>();
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
        //if (currentHitPoints <= 0)
            //GameManager.Instance.OnEntityDie(this);
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
