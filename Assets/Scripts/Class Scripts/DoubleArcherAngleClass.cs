using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleArcherAngleClass : CharacterClass
{
    public Projectile bulletPrefab;
    public GameObject shootPointSide;
    public GameObject shootPointUp;
    public float shootForce = 1f;

    public override void Attack()
    {
        // bullet to the right
        Projectile bulletInstance;
        bulletInstance = Instantiate(bulletPrefab, shootPointSide.transform.position, transform.rotation);
        bulletInstance.rb.AddForce(shootPointSide.transform.right * shootForce, ForceMode2D.Impulse);
        // bullet up
        bulletInstance = Instantiate(bulletPrefab, shootPointUp.transform.position, transform.rotation);
        bulletInstance.rb.AddForce(shootPointUp.transform.up * shootForce, ForceMode2D.Impulse);
    }
}
