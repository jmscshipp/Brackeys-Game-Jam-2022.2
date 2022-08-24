using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Selector playerSelector;
    bool placed;
    Vector3 startPos; // where character was instantiated off the grid
    LevelManager manager;

    void Start()
    {
        placed = false;
        manager = GameObject.Find("LEVELMANAGER").GetComponent<LevelManager>();
        playerSelector = GameObject.Find("SelectionControl").GetComponent<Selector>();
        startPos = transform.position;
    }

    // called by selector when character is put on tile
    public void Placed()
    {
        if (!placed)
        {
            manager.CharacterPlaced();
            placed = true;
        }
    }

    // called by selector when character is dropped anywhere not on the grid
    public void ResetPos()
    {
        transform.position = startPos;
        if (placed)
        {
            manager.CharacterRemoved();
            placed = false;
        }
    }

    private void OnMouseOver()
    {
        playerSelector.SetSelectedCharacter(gameObject);
    }

    private void OnMouseExit()
    {
        playerSelector.SetSelectedCharacter(null);
    }
}
