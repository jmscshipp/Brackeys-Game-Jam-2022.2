using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice : Weapon
{
    public override void Death()
    {
        StartCoroutine(DeathCountown());
    }

    public override void Deflect()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        transform.position -= transform.right;
    }

    IEnumerator DeathCountown()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
