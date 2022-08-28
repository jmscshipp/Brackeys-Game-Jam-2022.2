using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleArcherStraightClass : CharacterClass
{
    public Projectile bulletPrefab;
    public GameObject shootPointRight;
    public GameObject shootPointLeft;
    public float shootForce = 1f;

    public override void Attack()
    {
        // bullet to the right
        Projectile bulletInstance;
        bulletInstance = Instantiate(bulletPrefab, shootPointRight.transform.position, shootPointRight.transform.rotation);
        bulletInstance.rb.AddForce(shootPointRight.transform.right * shootForce, ForceMode2D.Impulse);
        // bullet left
        bulletInstance = Instantiate(bulletPrefab, shootPointLeft.transform.position, shootPointLeft.transform.rotation);
        bulletInstance.rb.AddForce(shootPointLeft.transform.right * shootForce, ForceMode2D.Impulse);
    }
}
