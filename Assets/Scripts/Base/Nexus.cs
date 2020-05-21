using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : Entity
{
    [SerializeField]
    private Transform[] _navPoints = null;

    public Transform[] NavPoints => _navPoints;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if(CurrentHitPoints <= 0)
            GameManager.Instance.OnGameEnd();
    }

}
