using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEntityData", menuName = "Data/Entity Data")]
public class EntityData : ScriptableObject
{
    [SerializeField]
    private int _maxHitPoints = 0;
    [SerializeField]
    private float _baseSpeed = 0;
    [SerializeField]
    private float _attackRange = 0;
    [SerializeField]
    private string _defaultTargetTag = string.Empty;

    public int MaxHitPoints => _maxHitPoints;
    public float BaseSpeed => _baseSpeed;
    public float AttackRange => _attackRange;
    public string DefaultTargetTag => _defaultTargetTag;
}
