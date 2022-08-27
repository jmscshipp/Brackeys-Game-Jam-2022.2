using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    SpriteRenderer renderer;
    Selector playerSelector;

    Color goalColor;

    public float colorLerpTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        playerSelector = GameObject.Find("SelectionControl").GetComponent<Selector>();
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (goalColor != renderer.color)
        {
            colorLerpTime += Time.deltaTime * 0.25f;
            renderer.color = Color.Lerp(renderer.color, goalColor, colorLerpTime);
        }
    }

    private void OnMouseEnter()
    {
        colorLerpTime = 0f;
        goalColor = Color.gray;
        playerSelector.SetHoveringTile(this);
    }

    private void OnMouseExit()
    {
        colorLerpTime = 0f;
        goalColor = Color.clear;
        playerSelector.SetHoveringTile(null);
    }
}
