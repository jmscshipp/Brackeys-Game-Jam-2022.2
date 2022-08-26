using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryCharacter : MonoBehaviour
{
    Selector playerSelector;
    bool placed;
    Vector3 startPos; // where character was instantiated off the grid
    LevelManager manager;
    CameraControl camControl;
    CharacterClass myClass;

    public int health = 1;
    public Animator anim;

    void Start()
    {
        placed = false;
        manager = GameObject.Find("LEVELMANAGER").GetComponent<LevelManager>();
        playerSelector = GameObject.Find("SelectionControl").GetComponent<Selector>();
        camControl = Camera.main.GetComponent<CameraControl>();
        myClass = GetComponent<CharacterClass>();
        startPos = transform.position;
        manager.CharacterPlaced(gameObject);
    }

    private void OnMouseOver()
    {
        anim.Play("HoveredAnim");
    }

    private void OnMouseExit()
    {
        if(GetComponent<BoxCollider2D>().enabled == true)
        {
            anim.Play("Unhover");
        }

    }

    public void Attack()
    {
        myClass.Attack();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log(gameObject.name + "collided with " + other.gameObject.name);
        if (other.tag == "Attack")
        {
            health -= other.GetComponent<Weapon>().damage;
            other.GetComponent<Weapon>().Death();
            if (health <= 0)
            {
                camControl.TriggerHeavyScreenShake();
                manager.CharacterKilled();
                Destroy(gameObject);
            }
        }
    }
}
