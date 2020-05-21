using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    #region Serialized Fields

    [Header("Entity Properties")]
    [SerializeField]
    private EntityData _entityData = null;

    [Header("Graphical Properties")]
    [SerializeField]
    private StatusText statusText = null;
    [SerializeField]
    private DamageText[] damageText = null;
    [SerializeField]
    private Image hpBarFill = null;

    #endregion

    #region Fields
    #endregion

    #region Auto Properties

    public float CurrentHitPoints { get; private set; }
    public float MovementMultiplier { get; protected set; }
    public List<StatusEffect> StatusEffects { get; protected set; }

    public ObjectPooler ObjectPooler { get; private set; }

    #endregion

    #region Full Properties

    private int currentDamageText;
    public EntityData EntityData => _entityData;

    #endregion

    #region Unity Callbacks

    protected virtual void Awake()
    {
        CurrentHitPoints = EntityData.MaxHitPoints;
        currentDamageText = 0;
        MovementMultiplier = 1;
        ObjectPooler = GetComponent<ObjectPooler>();
    }
    
    #endregion

    #region Public Methods

    public void SetMovementMultiplier(float newMultiplier)
    {
        MovementMultiplier = newMultiplier;
    }
    public virtual void TakeDamage(float damage)
    {
        CurrentHitPoints -= damage;
        DisplayDamageText(damage);
        UpdateHPBar();
    }
    public virtual void SetStatus(StatusEffect newStatus)
    {
        if (!StatusEffects.Contains(newStatus))
        {
            statusText.Activate(newStatus);
            StatusEffects.Add(newStatus);
            StatusEffects[StatusEffects.Count - 1].OnStart(this);
        }
    }

    #endregion

    #region Private Methods

    private void DisplayDamageText(float damage)
    {
        damageText[currentDamageText].gameObject.SetActive(true);
        damageText[currentDamageText].SetText(damage);

        currentDamageText++;
        if (currentDamageText == damageText.Length)
            currentDamageText = 0;

    }

    private void UpdateHPBar()
    {
        float percentage = CurrentHitPoints / EntityData.MaxHitPoints;
        hpBarFill.fillAmount = percentage;
    }
    #endregion

    #region Protected Methods

    protected void UpdateStatus()
    {
        if (StatusEffects != null)
        {
            for (int i = 0; i < StatusEffects.Count; i++)
            {
                StatusEffects[i].DoStatusEffect(Time.deltaTime);
            }
        }
    }

    #endregion
}
