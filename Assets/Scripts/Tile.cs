using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    SpriteRenderer renderer;
    Selector playerSelector;

    // Start is called before the first frame update
    void Start()
    {
        playerSelector = GameObject.Find("SelectionControl").GetComponent<Selector>();
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        renderer.color = Color.blue;
        playerSelector.SetHoveringTile(this);
    }

    private void OnMouseExit()
    {
        renderer.color = Color.white;
        playerSelector.SetHoveringTile(null);
    }
}
