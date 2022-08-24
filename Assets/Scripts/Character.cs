using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Selector playerSelector;

    void Start()
    {
        playerSelector = GameObject.Find("SelectionControl").GetComponent<Selector>();
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
