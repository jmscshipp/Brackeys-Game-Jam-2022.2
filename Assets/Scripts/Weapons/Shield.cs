using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Attack" || collision.tag == "Healing")
        {
            collision.GetComponent<Weapon>().Deflect();
            gameObject.SetActive(false);
        }
    }
}
