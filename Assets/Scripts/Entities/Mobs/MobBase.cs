using StateMachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobBase : Entity
{
    #region Serialized Fields

    [SerializeField]
    private EnemyAttack _attack = null;

    #endregion

    #region Fields
    #endregion

    #region Auto Properties

    public StateMachine<MobBase> StateMachine { get; private set; }
    public NavMeshAgent NavMeshAgent { get; protected set; }

    public Transform CurrentTarget { get; protected set; }
    public Transform DefaultTarget { get; protected set; }

    #endregion

    #region Full Properties

    public EnemyAttack Attack => _attack;

    #endregion

    #region Unity Callbacks
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new StateMachine<MobBase>(this);
        StatusEffects = new List<StatusEffect>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Attack.Initialisation(this);
    }

    protected virtual void Start()
    {
        NavMeshAgent.stoppingDistance = EntityData.AttackRange;
        NavMeshAgent.speed = EntityData.BaseSpeed;
    }

    #endregion

    #region Public Methods
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (CurrentHitPoints <= 0 && gameObject.activeSelf)
        {
            GameManager.Instance.EntityManager.OnEntityDie();
            gameObject.SetActive(false);
        }
    }

    public void SwitchTarget(Entity newTarget)
    {
        CurrentTarget = newTarget.gameObject.transform;
    }

    public override void SetStatus(StatusEffect newStatus)
    {
        base.SetStatus(newStatus);
        NavMeshAgent.speed = EntityData.BaseSpeed * MovementMultiplier;
    }

    #endregion

    #region Private Methods

    #endregion

    #region Protected Methods


    #endregion
}
