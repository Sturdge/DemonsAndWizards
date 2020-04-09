using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WaveElement
{
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private int _number;

    public GameObject Prefab => _prefab;
    public int Number => _number;
}