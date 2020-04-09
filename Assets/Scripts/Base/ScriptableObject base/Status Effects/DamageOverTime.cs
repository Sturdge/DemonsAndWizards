using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageOverTime", menuName = "Status Effects/Damage Over Time")]
public class DamageOverTime : StatusEffect
{
    private float interval;
    private float period;

    public override void OnStart(Entity parent)
    {
        base.OnStart(parent);
        interval = 1;
        period = interval;
        Debug.Log("Burned!");
    }

    public override void DoStatusEffect(float deltaTime)
    {
        base.DoStatusEffect(deltaTime);
        period += deltaTime;
        if (period >= interval)
        {
            Parent.TakeDamage(1);
            period = 0;
        }
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }
}