using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsmanClass : CharacterClass
{
    public Projectile slicePrefab;
    public GameObject slicePoint;
    public float shootForce = 1f;

    public override void Attack()
    {
        Projectile bulletInstance;
        bulletInstance = Instantiate(slicePrefab, slicePoint.transform.position, transform.rotation);
        bulletInstance.rb.AddForce(slicePoint.transform.right * shootForce, ForceMode2D.Impulse);
    }
}
