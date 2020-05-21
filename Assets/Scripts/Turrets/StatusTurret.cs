using UnityEngine;

public class StatusTurret : Turret
{
    [SerializeField]
    private StatusEffect effect;

    protected override void TurretBehaviour()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (!targets[i].StatusEffects.Contains(effect))
            {
                targets[i].SetStatus(effect);
            }
        }
    }
}
