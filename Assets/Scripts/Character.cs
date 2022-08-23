using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    bool isHeld = false;

    Vector2 mousePos;

    List<GameObject> surroundingTiles = new List<GameObject>();
    Camera cam;


    private void Start() 
    {
        cam = FindObjectOfType<Camera>();
    }

    void OnMouseDrag() 
    {
        Debug.Log("dragging");
        isHeld = true;
        transform.position = mousePos;
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.GetComponent<Tile>() != null)
        {
            surroundingTiles.Add(other.gameObject);
            
        }
        Debug.Log("tileadded");
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.GetComponent<Tile>() != null)
        {
            surroundingTiles.Remove(other.gameObject);
            
        }
        Debug.Log("tileremoved");
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp() 
    {
        if (isHeld)
        {
            float shortestDistance = 9999999999;
            GameObject closestTile = null;
            foreach (var nearTile in surroundingTiles)
            {
                var distance = Mathf.Abs(Vector2.Distance(nearTile.transform.position, transform.position));
                if (shortestDistance > distance)
                {
                    shortestDistance = distance;
                    closestTile = nearTile;
                }
            }
            isHeld = false;
            transform.position = closestTile.transform.position;
        }
    }
}
