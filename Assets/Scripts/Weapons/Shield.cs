using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    LevelManager manager;

    private void Start()
    {
        manager = GameObject.Find("LEVELMANAGER").GetComponent<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Attack" || collision.tag == "Healing")
        {
            collision.GetComponent<Weapon>().Deflect();
            manager.ValidateTurn();
            gameObject.SetActive(false);
        }
    }
}
