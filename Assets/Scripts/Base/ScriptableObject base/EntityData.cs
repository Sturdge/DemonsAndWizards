using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEntityData", menuName = "Data/Entity Data")]
public class EntityData : ScriptableObject
{
    [SerializeField]
    private int _maxHitPoints;
    [SerializeField]
    private float _baseSpeed;

    public int MaxHitPoints => _maxHitPoints;
    public float BaseSpeed => _baseSpeed;
}
