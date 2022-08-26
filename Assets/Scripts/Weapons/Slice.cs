using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice : Weapon
{
    public override void Death()
    {
        StartCoroutine(DeathCountown());
    }

    IEnumerator DeathCountown()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
