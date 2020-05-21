using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WaveElement
{
    [SerializeField]
    private UnityEngine.GameObject _prefab = null;
    [SerializeField]
    private int _number = 0;

    public UnityEngine.GameObject Prefab => _prefab;
    public int Number => _number;
}