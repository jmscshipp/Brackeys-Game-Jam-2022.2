using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Weapon
{
    public SpriteRenderer renderer;

    public override void Death()
    {
        Destroy(gameObject);
    }

    public override void Deflect()
    {
        renderer.flipX = true;
        GetComponent<Rigidbody2D>().velocity *= -1;
    }
}
