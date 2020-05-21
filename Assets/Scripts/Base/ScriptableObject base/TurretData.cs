using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTurretData", menuName = "Data/Turret Data")]
public class TurretData : ScriptableObject
{
    [SerializeField]
    private string _turretName;
    [SerializeField]
    private int _cost;
    [SerializeField]
    private int _maxHP;
    [SerializeField]
    private float _cooldown;

    public string TurretName => _turretName;
    public int Cost => _cost;
    public int MaxHP => _maxHP;
    public float Cooldown => _cooldown;
}
