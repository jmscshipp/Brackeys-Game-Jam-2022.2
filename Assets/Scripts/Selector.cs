using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    Camera cam;
    GameObject selectedCharacter;
    Tile hoveringTile;
    bool carrying;

    void Start()
    {
        carrying = false;
        cam = Camera.main;
    }

    void Update()
    {
        if (carrying) // character follows mouse
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            selectedCharacter.transform.position = new Vector3(mousePos.x, mousePos.y, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && selectedCharacter != null)
        {
            carrying = true;
            selectedCharacter.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && selectedCharacter != null)
        {
            if (hoveringTile == null) // not over tile, reset pos
            {

            }
            else // over tile, set to tile pos
                selectedCharacter.transform.position = hoveringTile.transform.position;

            selectedCharacter.GetComponent<BoxCollider2D>().enabled = true;
            carrying = false;
            selectedCharacter = null;
        }
    }

    // called by characters when mouse hovers over them
    public void SetSelectedCharacter(GameObject currentCharacter)
    {
        if (!carrying)
            selectedCharacter = currentCharacter;
    }

    // called by tiles when mouse hovers over them
    public void SetHoveringTile(Tile currentTile)
    {
        hoveringTile = currentTile;
    }
}
