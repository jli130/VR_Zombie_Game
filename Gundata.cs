using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Gun", menuName ="Weapon/Gun")]
public class Gundata : ScriptableObject
{
    // Start is called before the first frame update
    [Header("Info")]
    public new string name;

    [Header("Shooting")]
    public float maxDistance;

    [Header("Reloading")]
    public int magSize;
    public int curAmmo;
    public float fireRate;
    public float reloadTime;
    [HideInInspector]
    public bool reloading;
}
