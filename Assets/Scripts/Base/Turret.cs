using System.Collections.Generic;
using UnityEngine;

public abstract class Turret : MonoBehaviour
{

    [SerializeField]
    private TurretData _data;

    protected List<MobBase> targets;

    public TurretData Data => _data;

    private void Awake()
    {
        targets = new List<MobBase>();
    }

    private void Update()
    {
        if (targets.Count > 0)
        {
            TurretBehaviour();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targets.Add(other.GetComponent<MobBase>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        MobBase entity = other.GetComponent<MobBase>();

        if (targets.Contains(entity))
        {
            targets.Remove(entity);
        }
    }

    protected abstract void TurretBehaviour();
}