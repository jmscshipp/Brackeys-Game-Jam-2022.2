using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // not used for swordsman
    public Rigidbody2D rb;

    public int damage = 1;
    public bool isExplosive = false;

    public abstract void Death();
}
