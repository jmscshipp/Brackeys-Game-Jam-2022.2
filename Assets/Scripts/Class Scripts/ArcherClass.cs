using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherClass : CharacterClass
{
    public Projectile bulletPrefab;
    public GameObject shootPoint;
    public float shootForce = 1f;

    public override void Attack()
    {
        Projectile bulletInstance;
        bulletInstance = Instantiate(bulletPrefab, shootPoint.transform.position, transform.rotation);
        bulletInstance.rb.AddForce(shootPoint.transform.right * shootForce, ForceMode2D.Impulse);
    }
}
