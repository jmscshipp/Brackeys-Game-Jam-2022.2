using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBoundaries : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
            if (other.tag == "Attack" || other.tag == "Healing")
            {
                Destroy(other.gameObject);
            }
    }
}
