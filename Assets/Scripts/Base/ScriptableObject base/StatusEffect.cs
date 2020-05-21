using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    [SerializeField]
    private string _name = string.Empty;
    [SerializeField]
    private Color _textColour = Color.white;
    [SerializeField]
    protected int duration = 0;

    protected float elapsedTime;

    public string Name => _name;
    public Color TextColour => _textColour;

    public Entity Parent { get; protected set; }

    public virtual void OnStart(Entity parent)
    {
        Parent = parent;
        elapsedTime = 0;
    }

    public virtual void DoStatusEffect(float deltaTime)
    {
        elapsedTime += deltaTime;
        if (elapsedTime >= duration)
            OnEnd();
    }

    public virtual void OnEnd() => Parent.StatusEffects.Remove(this);
}