using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeedModifier", menuName = "Status Effects/Speed Modifier")]
public class SpeedModifier : StatusEffect
{
    [SerializeField]
    private float modifier;

    public override void OnStart(Entity parent)
    {
        base.OnStart(parent);
        Parent.SetMovementMultiplier(modifier);
    }

    public override void DoStatusEffect(float deltaTime)
    {
        base.DoStatusEffect(deltaTime);
    }

    public override void OnEnd()
    {
        Parent.SetMovementMultiplier(1);
        base.OnEnd();
    }
}