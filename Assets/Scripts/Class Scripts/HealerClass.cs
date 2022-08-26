using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerClass : CharacterClass
{
    public Projectile healingOrbPrefab;
    public GameObject shootPoint;
    public float shootForce = 1f;

    public override void Attack()
    {
        Projectile healOrbInstance;
        healOrbInstance = Instantiate(healingOrbPrefab, shootPoint.transform.position, transform.rotation);
        healOrbInstance.rb.AddForce(shootPoint.transform.right * shootForce, ForceMode2D.Impulse);
    }
}
