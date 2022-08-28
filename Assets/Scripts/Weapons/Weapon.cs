using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // not used for swordsman
    public Rigidbody2D rb;

    public int damage = 1;
    public bool isExplosive = false;
    LevelManager manager;

    private void Start()
    {
        manager = GameObject.Find("LEVELMANAGER").GetComponent<LevelManager>();
        manager.AttackSpawned();
    }

    private void OnDestroy()
    {
        manager.AttackDespawned();
    }

    public abstract void Death();
    public abstract void Deflect();
}
