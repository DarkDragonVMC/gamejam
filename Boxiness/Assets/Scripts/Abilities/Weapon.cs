using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public new string name;

    public float delay;
    public int damage;
    public float speed;
    public float range;
    public Vector2 bloom;

    public bool bouncy;
    public bool damageSelf;

    public bool explosive;
    public float expRadius;

    public float roatateSpeed;

    public bool stuck;
    public bool path;

    public int bulletsPerTap;
    public float burstDelay;

    public GameObject bulletSkin;
}
