using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    Camera cam;
    GameObject selectedCharacter;
    Tile hoveringTile;
    bool carrying;
    CharacterBank bank;

    void Start()
    {
        carrying = false;
        cam = Camera.main;
        bank = GameObject.Find("CHARACTERBANK").GetComponent<CharacterBank>();
    }

    void Update()
    {
        if (carrying) // character follows mouse
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            selectedCharacter.transform.position = new Vector3(mousePos.x, mousePos.y, -1.0f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && selectedCharacter != null)
        {
            carrying = true;
            bank.RemoveFromBank(selectedCharacter);
            selectedCharacter.GetComponent<BoxCollider2D>().enabled = false; // turn off character's collider until we drop it to avoid OnMouseOver being blocked for tiles
            selectedCharacter.GetComponent<Animator>().Play("HeldAnim");
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && selectedCharacter != null) // for rotating character
        {
            selectedCharacter.GetComponent<Character>().Rotate();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && selectedCharacter != null)
        {

            if (hoveringTile == null) // not over tile, reset pos
            {
                selectedCharacter.GetComponent<Character>().Reset();
                bank.AddToBank(selectedCharacter);
            }
            else // over tile, set to tile pos
            {
                selectedCharacter.GetComponent<Character>().Placed();
                selectedCharacter.transform.position = new Vector3(hoveringTile.transform.position.x, hoveringTile.transform.position.y, -1.0f);
            }

            // reset what is our selected character
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
