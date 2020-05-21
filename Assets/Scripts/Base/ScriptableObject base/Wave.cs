using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWave", menuName = "Data/Wave")]
public class Wave : ScriptableObject
{
    [SerializeField]
    private WaveElement[] _waveData = null;

    public WaveElement[] WaveData => _waveData;
}