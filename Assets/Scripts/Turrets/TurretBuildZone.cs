using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuildZone : MonoBehaviour
{

    [SerializeField]
    private List<Turret> turrets;

    public int builtTurret { get; private set; }

    public List<Turret> Turrets => turrets;

    private void Awake()
    {
        builtTurret = -1;
    }

    public void BuildTurret(int i)
    {
        builtTurret = i;
        turrets[i].gameObject.SetActive(true);
    }

}
