using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Weapon
{
    public override void Death()
    {
        Destroy(gameObject);
    }

    public override void Deflect()
    {
        GetComponent<Rigidbody2D>().velocity *= -1;
    }
}
